using Autofac;
using Business.Abstract;
using Business.DependencyResolvers.Autofac;
using Business.DeviceIdentifier;
using Core.ActionReports;
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
        public IFieldHourArchiveParameterService _fieldHourArchiveParameterService;
        SemaphoreSlim _semaphore;
        List<List<FieldHourlyArchiveParameter>> _fieldHourlyArchiveParameters;
        public HourlyArchiveParameterManager()
        {
            _semaphore = new SemaphoreSlim(10);
            _fieldHourlyArchiveParameters = new List<List<FieldHourlyArchiveParameter>>();
        }

        public async Task GetHourArchivesFromDeviceAsync(List<DeviceParameter> deviceParameters)
        {
            _fieldHourlyArchiveParameters.Clear();
            foreach (var deviceParameter in deviceParameters)
            {
                await _semaphore.WaitAsync(50);


                var progress = new Progress<ProgressStatus>(deviceParameter.ProgressReportAction);
               

                _fieldHourArchiveParameterService = AutofacBusinessContainerBuilder.AutofacBusinessContainer.Resolve<IFieldHourArchiveParameterService>();
                _fieldHourArchiveParameterService.OnFieldDataIsReadyEvent += _fieldHourArchiveParameterService_OnFieldDataIsReadyEvent;
                _fieldHourArchiveParameterService.GetHourArchiveFromDeviceAsync(correctorMaster, progress);
            }
        }

        private void Progress_ProgressChanged(object sender, ProgressStatus e)
        {
            throw new NotImplementedException();
        }

        private void _fieldHourArchiveParameterService_OnFieldDataIsReadyEvent(object sender, List<FieldHourlyArchiveParameter> e)
        {
            _fieldHourlyArchiveParameters.Add(e);
        }
    }
}
