using Core.ActionReports;
using Core.Events.Results;
using FieldBusiness.Abstract;
using FieldEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldBusiness.Concrete
{
    public class FieldCurrentParameterManager : IFieldCurrentParameterService
    {
        public event EventHandler<FieldEventResult<FieldCurrentParameter, IProgress<ProgressStatus>>> OnFieldDataIsReadyEvent;
    }
}
