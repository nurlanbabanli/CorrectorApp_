using Business.Events.Abstract;
using Business.Utilities;
using Core.Results.Abstract;
using Core.Utilities.FieldDeviceIdentifier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICurrentParameterService: ICurrentParameterResultEvent<IDataResult<List<CurrentParameterHolder>>>
    {
        Task GetCurrentParameterFromDeviceAsync(DataTransmissionParametersHolderList deviceParameters);
        Task GetCurrentParameterFromMqttBroker(string MqttTopic);
    }
}
