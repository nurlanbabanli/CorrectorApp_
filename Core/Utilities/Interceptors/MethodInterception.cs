﻿using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Events.Abstract;
using Core.Results.Abstract;
using Core.Results.Concrete;
using Core.Tools;
using Core.Utilities.FieldDeviceIdentifier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
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
            CommonExceptionLogger commonExceptionLogger = new CommonExceptionLogger(typeof(FileLogger));
            commonExceptionLogger.Log(invocation, exception);
        }
    }
}
