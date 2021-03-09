using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class DailyArchiveParameter : IEntity
    {
        public int DeviceId { get; set; }

        // Rest of Daily archive parameters will be added later
    }
}
