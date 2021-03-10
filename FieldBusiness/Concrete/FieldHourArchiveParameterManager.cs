using Core.ActionReports;
using Core.Utilities.DeviceIdentifier;
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

        public async Task GetHourArchiveFromDeviceAsync(IDeviceParameter deviceParameter, IProgress<ProgressStatus> progress)
        {
            List<FieldHourlyArchiveParameter> result = await _fieldHourlyArchiveParameterDal.GetFieldArchiveParametersAsync(deviceParameter,progress);
            OnFieldDataIsReadyEvent.Invoke(this, result);
        }
    }
}
