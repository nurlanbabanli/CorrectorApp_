using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.FieldDeviceIdentifier
{
    public class DeviceParameters
    {
        public int Id { get; set; }
        public string MeterCode { get; set; }
        public string SerialNumber { get; set; }
        public string DeviceName { get; set; }
        public string DeviceType { get; set; }
        public string IpAddresss { get; set; }
        public int DeviceAddress { get; set; }
        public string MqttTopic { get; set; }
        public bool IsService { get; set; }
        public bool IsActive { get; set; }
        public bool IsCorrector { get; set; }
    }
}
