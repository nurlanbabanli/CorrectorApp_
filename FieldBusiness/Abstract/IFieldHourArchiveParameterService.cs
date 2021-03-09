using Core.Events.Abstract;
using Entities.Concrete;
using FieldEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldBusiness.Abstract
{
    public interface IFieldHourArchiveParameterService : IResultEvent<FieldHourlyArchiveParameter>
    {
        Task GetHourArchiveFromDeviceAsync(CorrectorMaster correctorMaster);
    }
}
