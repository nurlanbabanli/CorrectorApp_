using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class CorrectorMaster : IEntity
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string MeterCode { get; set; }
        public string SerialNumber { get; set; }
        public string DeviceName { get; set; }
        public int DeviceType { get; set; }
        public int StationId { get; set; }
        public string IpAddress { get; set; }
        public int DeviceNo { get; set; }
        public int DeviceAddress { get; set; }
        public int DeviceGroup { get; set; }
        public string MqttTopic { get; set; }
        public bool IsService { get; set; }
        public bool IsActive { get; set; }
        public bool IsCorrector { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
    }
}
