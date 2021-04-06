using Core.ActionReports;
using Core.Aspects.Autofac.Validation;
using Core.Events.Results;
using Core.Utilities.FieldDeviceIdentifier;
using FieldBusiness.Abstract;
using FieldBusiness.ValidationRules.FluentValidation;
using FieldDataAccess.Abstract;
using FieldEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldBusiness.Concrete
{
    public class FieldDailyArchiveParameterManager : IFieldDailyArchiveParameterService
    {
        public event EventHandler<FieldEventResult<FieldDailyArchiveParameter, IProgress<ProgressStatus>>> OnFieldDataIsReadyEvent;
        IFieldDailyArchiveParameterDal _fieldDailyArchiveParameterDal;

        public FieldDailyArchiveParameterManager(IFieldDailyArchiveParameterDal fieldDailyArchiveParameterDal)
        {
            _fieldDailyArchiveParameterDal = fieldDailyArchiveParameterDal;
        }

        [ValidationAspect(typeof(ArchiveDataTransmissionParameterHolderValidator), Priority = 1)]
        public async Task GetArchiveFromDeviceAsync(DataTransmissionParameterHolder deviceParameter)
        {
            List<FieldDailyArchiveParameter> result = await _fieldDailyArchiveParameterDal.GetFieldArchiveParametersAsync(deviceParameter);
            var fieldEventResult = new FieldEventResult<FieldDailyArchiveParameter, IProgress<ProgressStatus>>()
            {
                DataList = result,
                Progress = deviceParameter.UserInterfaceParametersHolder.ProgressReport,
                DeviceId = deviceParameter.DeviceParametersHolder.Id
            };
            OnFieldDataIsReadyEvent.Invoke(this, fieldEventResult);
        }
    }
}
