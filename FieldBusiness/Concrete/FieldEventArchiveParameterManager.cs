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
    public class FieldEventArchiveParameterManager : IFieldEventArchiveParameterService
    {
        public event EventHandler<FieldEventResult<FieldEventArchiveParameter, IProgress<ProgressStatus>>> OnFieldDataIsReadyEvent;
        IFieldEventArchiveParameterDal _fieldEventArchiveParameterDal;

        public FieldEventArchiveParameterManager(IFieldEventArchiveParameterDal fieldEventArchiveParameterDal)
        {
            _fieldEventArchiveParameterDal = fieldEventArchiveParameterDal;
        }

        [ValidationAspect(typeof(ArchiveDataTransmissionParameterHolderValidator), Priority = 1)]
        public async Task GetEventArchiveFromDeviceAsync(DataTransmissionParameterHolder deviceParameter)
        {
            List<FieldEventArchiveParameter> result =await _fieldEventArchiveParameterDal.GetFieldArchiveParametersAsync(deviceParameter);
            var fieldEventResult = new FieldEventResult<FieldEventArchiveParameter, IProgress<ProgressStatus>>()
            {
                DataList = result,
                Progress = deviceParameter.UserInterfaceParametersHolder.ProgressReport,
                DeviceId = deviceParameter.DeviceParametersHolder.Id
            };
            OnFieldDataIsReadyEvent.Invoke(this, fieldEventResult);
        }
    }
}
