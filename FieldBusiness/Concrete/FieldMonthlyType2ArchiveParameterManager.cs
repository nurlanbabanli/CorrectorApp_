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
    public class FieldMonthlyType2ArchiveParameterManager : IFieldMonthlyType2ArchiveParameterService
    {
        public event EventHandler<FieldEventResult<FieldMonthlyType2ArchiveParameter, IProgress<ProgressStatus>>> OnFieldDataIsReadyEvent;
        IFieldMonthlyType2ArchiveParameterDal _fieldMonthlyType2ArchiveParameterDal;

        public FieldMonthlyType2ArchiveParameterManager(IFieldMonthlyType2ArchiveParameterDal fieldMonthlyType2ArchiveParameterDal)
        {
            _fieldMonthlyType2ArchiveParameterDal = fieldMonthlyType2ArchiveParameterDal;
        }
        [ValidationAspect(typeof(ArchiveDataTransmissionParameterHolderValidator), Priority = 1)]
        public async Task GetArchiveFromDeviceAsync(DataTransmissionParameterHolder deviceParameter)
        {
            List<FieldMonthlyType2ArchiveParameter> result = await _fieldMonthlyType2ArchiveParameterDal.GetFieldArchiveParametersAsync(deviceParameter);
            var fieldEventResult = new FieldEventResult<FieldMonthlyType2ArchiveParameter, IProgress<ProgressStatus>>()
            {
                DataList = result,
                Progress = deviceParameter.UserInterfaceParametersHolder.ProgressReport,
                DeviceId = deviceParameter.DeviceParametersHolder.Id
            };
            OnFieldDataIsReadyEvent.Invoke(this, fieldEventResult);
        }
    }
}
