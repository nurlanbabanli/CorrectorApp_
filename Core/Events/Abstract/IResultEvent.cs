using Core.Events.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Events.Abstract
{
    public interface IResultEvent<TData, TProgress>
    {
        event EventHandler<FieldEventResult<TData,TProgress>> OnFieldDataIsReadyEvent;
    }
}
