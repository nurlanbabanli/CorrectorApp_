using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Events.Abstract
{
    public interface IResultEvent<TEventData>
    {
        event EventHandler<List<TEventData>> OnFieldDataIsReadyEvent;
    }
}
