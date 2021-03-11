using Core.ActionReports;
using Core.Utilities.FieldDeviceIdentifier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsAppTest
{
    public class ParametersTransfer
    {
        public static DataTransmissionParameterHolder CombineData(ArchiveHandler archiveHandler)
        {
            var dataTransmissionParameterHolder = new DataTransmissionParameterHolder();
            dataTransmissionParameterHolder.DeviceParametersHolder = GetDeviceParameters(archiveHandler);
            dataTransmissionParameterHolder.UserInterfaceParametersHolder = GetUserInterfaceParameters(archiveHandler);
            dataTransmissionParameterHolder.ArchiveParametersHolder = GetArchiveParameters(archiveHandler);
            return dataTransmissionParameterHolder;
        }

        private static DeviceParameters GetDeviceParameters(ArchiveHandler archiveHandler)
        {
            var deviceParameters = new DeviceParameters();
            deviceParameters.Id = archiveHandler._correctorMaster.Id;
            deviceParameters.IpAddresss = archiveHandler._correctorMaster.IpAddresss;
            return deviceParameters;
        }

        private static UserInterfaceParameters GetUserInterfaceParameters(ArchiveHandler archiveHandler)
        {
            var userInterfaceParameters = new UserInterfaceParameters();
            userInterfaceParameters.ProgressReport = new Progress<ProgressStatus>(archiveHandler.Action);
            return userInterfaceParameters;
        }

        private static ArchiveParameters GetArchiveParameters(ArchiveHandler archiveHandler)
        {
            var archiveParameters = new ArchiveParameters();
            return archiveParameters;
        }
    }
}
