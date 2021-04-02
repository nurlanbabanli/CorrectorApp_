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
    public class FieldCurrentParameterManager : IFieldCurrentParameterService
    {
        public event EventHandler<FieldEventResult<FieldCurrentParameter, IProgress<ProgressStatus>>> OnFieldDataIsReadyEvent;
        IFieldCurrentParameterDal _fieldCurrentParameterServiceDal;

        public FieldCurrentParameterManager(IFieldCurrentParameterDal fieldCurrentParameterServiceDal)
        {
            _fieldCurrentParameterServiceDal = fieldCurrentParameterServiceDal;
        }

        [ValidationAspect(typeof(CurrentDataTransmissionParameterHolderValidator),Priority =1)]
        public async Task GetCurrentParametFromDeviceAsync(DataTransmissionParameterHolder deviceParameter)
        {
            List<FieldCurrentParameter> result = await _fieldCurrentParameterServiceDal.GetFieldArchiveParametersAsync(deviceParameter);
            var fieldEventResult = new FieldEventResult<FieldCurrentParameter, IProgress<ProgressStatus>>()
            {
                DataList = result,
                Progress = deviceParameter.UserInterfaceParametersHolder.ProgressReport,
                DeviceId = deviceParameter.DeviceParametersHolder.Id
            };
            OnFieldDataIsReadyEvent.Invoke(this, fieldEventResult);
        }
    }
}
