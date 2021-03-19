using Core.ActionReports;
using Core.Aspects.Autofac.Validation;
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
    public class FieldHourArchiveParameterManager : IFieldHourArchiveParameterService
    {
        public event EventHandler<List<FieldHourlyArchiveParameter>> OnFieldDataIsReadyEvent;

        IFieldHourlyArchiveParameterDal _fieldHourlyArchiveParameterDal;
        public FieldHourArchiveParameterManager(IFieldHourlyArchiveParameterDal fieldHourlyParameterDal)
        {
            _fieldHourlyArchiveParameterDal = fieldHourlyParameterDal;
        }

        [ValidationAspect(typeof(DataTransmissionParameterHolderValidator))]
        public async Task GetHourArchiveFromDeviceAsync(DataTransmissionParameterHolder deviceParameter)
        {
            List<FieldHourlyArchiveParameter> result = await _fieldHourlyArchiveParameterDal.GetFieldArchiveParametersAsync(deviceParameter);
           
            OnFieldDataIsReadyEvent.Invoke(this, result);
        }
    }
}
