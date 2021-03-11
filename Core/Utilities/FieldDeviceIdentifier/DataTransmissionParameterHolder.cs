using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Utilities.FieldDeviceIdentifier
{
    public class DataTransmissionParameterHolder
    {
        public ArchiveParameters ArchiveParametersHolder { get; set; }
        public DeviceParameters DeviceParametersHolder { get; set; }
        public UserInterfaceParameters UserInterfaceParametersHolder { get; set; }
        public SemaphoreSlim SemaphoreSlimT { get; set; }
    }
}
