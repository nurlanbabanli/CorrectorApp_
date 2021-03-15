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

        [ValidationAspect(typeof(DataTransmissionParameterHolderValidator))]
        public async Task GetHourArchivesFromDeviceAsync(List<DataTransmissionParameterHolder> deviceParameters)
        {
            _fieldHourlyArchiveParameters.Clear();
            var semaphoreSlim = ConcurrentTaskLimiter.GetSemaphoreSlim();
            IFieldHourArchiveParameterService fieldHourArchiveParameterService;

            foreach (var deviceParameter in deviceParameters)
            {
                deviceParameter.SemaphoreSlimT = semaphoreSlim;
                await deviceParameter.SemaphoreSlimT.WaitAsync();                             
                fieldHourArchiveParameterService = AutofacBusinessContainerBuilder.AutofacBusinessContainer.Resolve<IFieldHourArchiveParameterService>();

                GetHourArchiveFromDeviceAsync(deviceParameter, fieldHourArchiveParameterService);
            }
        }
     
        private void GetHourArchiveFromDeviceAsync(DataTransmissionParameterHolder deviceParameter, IFieldHourArchiveParameterService fieldHourArchiveParameterService)
        {
            //ValidationTool.Validate(new DeviceParameterValidator(), deviceParameter);

            fieldHourArchiveParameterService.OnFieldDataIsReadyEvent += FieldHourArchiveParameterService_OnFieldDataIsReadyEvent;
            fieldHourArchiveParameterService.GetHourArchiveFromDeviceAsync(deviceParameter);
        }

        private void FieldHourArchiveParameterService_OnFieldDataIsReadyEvent(object sender, List<FieldHourlyArchiveParameter> e)
        {
            _fieldHourlyArchiveParameters.Add(e);
        }
    }
}
