using Castle.DynamicProxy;
using Core.ActionReports;
using Core.CrossCuttingConcerns.Logging.Log4Net;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities.FieldDeviceIdentifier;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Core.Utilities.Interceptors
{
    public abstract class MethodInterception : MethodInterceptionBaseAttribute
    {

        protected virtual void OnBefore(IInvocation invocation) { }
        protected virtual void OnAfter(IInvocation invocation) { }
        protected virtual void OnException(IInvocation invocation, Exception exception) { }
        protected virtual void OnSuccess(IInvocation invocation) { }

        public override void Intercept(IInvocation invocation)
        {
            try
            {
                var isSuccess = true;
                OnBefore(invocation);
                try
                {
                    invocation.Proceed();
                    var result = invocation.ReturnValue as Task;
                    if ( result!=null && result.Exception!=null)
                    {
                        throw result.Exception;
                    }
                }
                catch (Exception e)
                {
                    isSuccess = false;
                    OnException(invocation, e);
                    ReturnMethodExceptions(invocation, e);
                    throw;
                }
                finally
                {
                    if (isSuccess)
                    {
                        OnSuccess(invocation);
                    }
                }
                OnAfter(invocation);
            }
            catch (Exception ex)
            {
                LogException(invocation, ex);
                ReturnMethodExceptions(invocation, ex);
                CallSemaphoreSlimRelase(invocation);
            }
        }
        

        private static void CallSemaphoreSlimRelase(IInvocation invocation)
        {
            invocation.ReturnValue = null;
            ParameterInfo[] parameterInfos = invocation.Method.GetParameters();

            if (parameterInfos.Length != 0 && typeof(DataTransmissionParameterHolder) == parameterInfos[0].ParameterType)
            {
                DataTransmissionParameterHolder dataTransmissionParameterHolder = (DataTransmissionParameterHolder)invocation.Arguments[0];
                dataTransmissionParameterHolder.SemaphoreSlimT.Release();
            }
        }

        private static void LogException(IInvocation invocation, Exception exception)
        {
            CommonExceptionLogger commonExceptionLogger = new CommonExceptionLogger(typeof(ExceptionFileLogger));
            commonExceptionLogger.Log(invocation, exception);
        }

        private static void ReturnMethodExceptions(IInvocation invocation, Exception exception)
        {
            if (invocation.Arguments.Length>=2)
            {
                IProgress<ProgressStatus> argument = null;
                foreach (var arg in invocation.Arguments)
                {
                    if (arg.GetType()==typeof(Progress<ProgressStatus>))
                    {
                        argument = (IProgress<ProgressStatus>)arg;
                        break;
                    }
                }               
                if (argument!=null && argument.GetType() == typeof(Progress<ProgressStatus>))
                {
                    IProgress<ProgressStatus> progress = (IProgress<ProgressStatus>)argument;
                    progress.Report(new ProgressStatus { StatusId = MessageStatus.Error, Message = exception.Message });
                } 
            }
        }
    }
}
