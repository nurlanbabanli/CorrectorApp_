using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net;
using Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging.Log4Net
{
    public class CommonExceptionLogger
    {
        private LoggerServiceBase _loggerServiceBase;
        public CommonExceptionLogger(Type loggerService)
        {
            if (loggerService.BaseType != typeof(LoggerServiceBase))
            {
                throw new System.Exception("Wrong Logger Type N");
            }

            _loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggerService);
        }

        public void Log(IInvocation invocation, System.Exception exception)
        {
            LogDetailWithException logDetailWithException = GetLogDetail(invocation);
            logDetailWithException.ExceptionMessage = exception.Message;
            logDetailWithException.MessageDateTime = FormatDateTime.FormatDateTimeAsString(DateTime.Now);
            _loggerServiceBase.Error(logDetailWithException);
        }

        private LogDetailWithException GetLogDetail(IInvocation invocation)
        {
            var logParameters = new List<LogParameter>();
            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameters.Add(new LogParameter
                {
                    Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Value = invocation.Arguments[i],
                    Type = invocation.Arguments[i].GetType().Name
                });
            }
            var logDetailWithException = new LogDetailWithException
            {
                MethodName = invocation.Method.Name,
                ClassName = invocation.TargetType.ToString(),
                LogParameters = logParameters
            };
            return logDetailWithException;
        }
    }
}
