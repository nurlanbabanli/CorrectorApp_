using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Events.Results
{
    public class FieldEventResult<TData,TProgress>
    {
        public List<TData> DataList { get; set; }
        public TProgress Progress { get; set; }
        public int DeviceId { get; set; }
    }
}
