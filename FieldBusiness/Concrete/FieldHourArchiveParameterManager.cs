using Entities.Concrete;
using FieldBusiness.Abstract;
using FieldDataAccess.Abstract;
using FieldEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldBusiness.Concrete
{
    public class FieldHourArchiveParameterManager : IFieldHourArchiveParameterService
    {
        public event EventHandler<List<FieldHourlyArchiveParameter>> OnFieldDataIsReadyEvent;

        IFieldHourlyArchiveParameterDal _fieldHourlyArchiveParameterDal;
        public FieldHourArchiveParameterManager(IFieldHourlyArchiveParameterDal fieldHourlyParameterDal)
        {
            _fieldHourlyArchiveParameterDal = fieldHourlyParameterDal;
        }

        public async Task GetHourArchiveFromDeviceAsync(CorrectorMaster correctorMaster)
        {
            List<FieldHourlyArchiveParameter> result = await _fieldHourlyArchiveParameterDal.GetFieldArchiveParametersAsync(correctorMaster);
            OnFieldDataIsReadyEvent.Invoke(this, result);
        }
    }
}
