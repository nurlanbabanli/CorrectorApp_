using Core.ActionReports;
using Core.Aspects.Autofac.Validation;
using Core.Events.Abstract;
using Core.Events.Results;
using Core.Results.Abstract;
using Core.Results.Concrete;
using Core.Utilities.FieldDeviceIdentifier;
using Entities.Concrete;
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
    public class FieldHourlyArchiveParameterManager : IFieldHourlyArchiveParameterService
    {
        public event EventHandler<FieldEventResult<FieldHourlyArchiveParameter, IProgress<ProgressStatus>>> OnFieldDataIsReadyEvent;

        IFieldHourlyArchiveParameterDal _fieldHourlyArchiveParameterDal;
        public FieldHourlyArchiveParameterManager(IFieldHourlyArchiveParameterDal fieldHourlyParameterDal)
        {
            _fieldHourlyArchiveParameterDal = fieldHourlyParameterDal;
        }
      
        [ValidationAspect(typeof(ArchiveDataTransmissionParameterHolderValidator),Priority =1)]
        public async Task GetArchiveFromDeviceAsync(DataTransmissionParameterHolder deviceParameter, IProgress<ProgressStatus> progress)
        {
            //throw new Exception("NNN");
            List<FieldHourlyArchiveParameter> result = await _fieldHourlyArchiveParameterDal.GetFieldArchiveParametersAsync(deviceParameter);
            var fieldEventResult = new FieldEventResult<FieldHourlyArchiveParameter, IProgress<ProgressStatus>>()
            {
                DataList = result,
                Progress = deviceParameter.UserInterfaceParametersHolder.ProgressReport,
                DeviceId= deviceParameter.DeviceParametersHolder.Id
            };
            OnFieldDataIsReadyEvent.Invoke(this, fieldEventResult);           
        }
    }
}
