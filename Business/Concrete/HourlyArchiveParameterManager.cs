using Business.Abstract;
using Business.DependencyResolvers.Autofac;
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

        public async Task GetHourArchivesFromDeviceAsync(List<CorrectorMaster> correctorMasters)
        {
            _fieldHourlyArchiveParameters.Clear();
            foreach (var correctorMaster in correctorMasters)
            {
                GetHourArchiveFromDevice(correctorMaster, new FieldHourArchiveParameterManager(new MbFieldHourlyArchiveParameterDal())); 

                //AutofacBusinessContainerBuilder.AutofacBusinessContainer.ResolveComponent()

                //GetHourArchiveFromDevice(correctorMaster, AutofacBusinessContainerBuilder.AutofacBusinessC)
                await _semaphore.WaitAsync(500);
            }
        }

        private void GetHourArchiveFromDevice(CorrectorMaster correctorMaster, IFieldHourArchiveParameterService fieldHourArchiveParameterService)
        {
            _fieldHourArchiveParameterService = fieldHourArchiveParameterService;
            _fieldHourArchiveParameterService.OnFieldDataIsReadyEvent += _fieldHourArchiveParameterService_OnFieldDataIsReadyEvent;
            _fieldHourArchiveParameterService.GetHourArchiveFromDeviceAsync(correctorMaster);
        }

        private void _fieldHourArchiveParameterService_OnFieldDataIsReadyEvent(object sender, List<FieldHourlyArchiveParameter> e)
        {
            _fieldHourlyArchiveParameters.Add(e);
        }
    }
}
