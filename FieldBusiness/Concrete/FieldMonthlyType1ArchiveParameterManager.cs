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
    public class FieldMonthlyType1ArchiveParameterManager : IFieldMonthlyType1ArchiveParameterService
    {
        public event EventHandler<FieldEventResult<FieldMonthlyType1ArchiveParameter, IProgress<ProgressStatus>>> OnFieldDataIsReadyEvent;
        IFieldMonthlyType1ArchiveParameterDal _fieldMonthlyType1ArchiveParameterDal;

        public FieldMonthlyType1ArchiveParameterManager(IFieldMonthlyType1ArchiveParameterDal fieldMonthlyType1ArchiveParameterDal)
        {
            _fieldMonthlyType1ArchiveParameterDal = fieldMonthlyType1ArchiveParameterDal;
        }
        [ValidationAspect(typeof(ArchiveDataTransmissionParameterHolderValidator), Priority = 1)]
        public async Task GetMonthlyType1ArchiveFromDeviceAsync(DataTransmissionParameterHolder deviceParameter)
        {
            List<FieldMonthlyType1ArchiveParameter> result = await _fieldMonthlyType1ArchiveParameterDal.GetFieldArchiveParametersAsync(deviceParameter);
            var fieldEventResult = new FieldEventResult<FieldMonthlyType1ArchiveParameter, IProgress<ProgressStatus>>()
            {
                DataList = result,
                Progress = deviceParameter.UserInterfaceParametersHolder.ProgressReport,
                DeviceId = deviceParameter.DeviceParametersHolder.Id
            };
            OnFieldDataIsReadyEvent.Invoke(this, fieldEventResult);
        }
    }
}
