using Autofac;
using Business.Abstract;
using Business.DependencyResolvers.Autofac;
using Business.Utilities;
using Business.ValidationRules.FluentValidation;
using Core.ActionReports;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.FluentValidation;
using Core.Utilities.FieldDeviceIdentifier;
using Entities.Concrete;
using FieldBusiness.Abstract;
using FieldBusiness.Concrete;
using FieldDataAccess.Concrete.Modbus;
using FieldEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class HourlyArchiveParameterManager : IHourlyArchiveParameterService
    {
        List<List<FieldHourlyArchiveParameter>> _fieldHourlyArchiveParameters;

        public HourlyArchiveParameterManager()
        {
            _fieldHourlyArchiveParameters = new List<List<FieldHourlyArchiveParameter>>();
        }

        [ValidationAspect(typeof(DataTransmissionParametersHolderListValidator))]
        public async Task GetHourArchivesFromDeviceAsync(DataTransmissionParametersHolderList deviceParameters)
        {
            _fieldHourlyArchiveParameters.Clear();
            var semaphoreSlim = ConcurrentTaskLimiter.GetSemaphoreSlim();
            foreach (var deviceParameter in deviceParameters.DataTransmissionParameterHolderList)
            {
                throw new Exception("Nurlan");
                deviceParameter.SemaphoreSlimT = semaphoreSlim;
                await deviceParameter.SemaphoreSlimT.WaitAsync();
                var fieldHourArchiveParameterService = AutofacBusinessContainerBuilder.AutofacBusinessContainer.Resolve<IFieldHourArchiveParameterService>();
                _ = fieldHourArchiveParameterService.GetHourArchiveFromDeviceAsync(deviceParameter);
                fieldHourArchiveParameterService.OnFieldDataIsReadyEvent += FieldHourArchiveParameterService_OnFieldDataIsReadyEvent;
                
            }
        }

        private void FieldHourArchiveParameterService_OnFieldDataIsReadyEvent(object sender, List<FieldHourlyArchiveParameter> e)
        {
            _fieldHourlyArchiveParameters.Add(e);
        }
    }
}
