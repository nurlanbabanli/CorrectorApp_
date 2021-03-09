using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class HourlyArchiveParameter : IEntity
    {
        public int DeviceId { get; set; }

        // Rest of hourly archive parameters will be added later
    }
}
