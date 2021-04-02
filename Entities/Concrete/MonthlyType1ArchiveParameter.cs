using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class MonthlyType1ArchiveParameter : IEntity
    {
        public int DeviceId { get; set; }
        public DateTime HistoryDateTime { get; set; }
        public int AbNo { get; set; }

        // Rest of MonthlyType1 archive parameters will be added later
    }
}
