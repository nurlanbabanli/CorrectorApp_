using Core.Utilities.FieldDeviceIdentifier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsAppTest
{
    public class DataTransfer
    {
        DataTransmissionParameterHolder CombineData(ArchiveHandler archiveHandler)
        {

        }

        private DeviceParameters GetDeviceParameters(ArchiveHandler archiveHandler)
        {
            var deviceParameters = new DeviceParameters();
            deviceParameters.Id = archiveHandler._correctorMaster.Id;
            deviceParameters.IpAddresss = archiveHandler._correctorMaster.IpAddresss;
        }


    }
}
