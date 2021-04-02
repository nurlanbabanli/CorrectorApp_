using Castle.DynamicProxy;
using Core.Utilities.FieldDeviceIdentifier;
using Core.Utilities.InMemoryLoggerParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging.Log4Net.Loggers
{
    public class InMemoryPeriodicLogger
    {
        public static void Log(IInvocation invocation, System.Exception exception)
        {
            foreach (var argument in invocation.Arguments)
            {
                if (argument.GetType()==typeof(DataTransmissionParameterHolder))
                {
                    DataTransmissionParameterHolder dataTransmissionParameterHolder = (DataTransmissionParameterHolder)argument;
                    InMemoryLoggedMessages.InMemoryMesssageLoggerParameters.Add(new InMemoryLoggerParameter { DeviceId = dataTransmissionParameterHolder.DeviceParametersHolder.Id, Messages = exception.Message });    
                    break;
                }
            }
        }
    }
}
