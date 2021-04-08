using Autofac;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.BusinessMessages;
using Business.DependencyResolvers.Autofac;
using Business.Helper.ParameterConverters;
using Business.Utilities;
using Business.Utilities.Security;
using Business.ValidationRules.FluentValidation;
using Core.ActionReports;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Results.Abstract;
using Core.Results.Concrete;
using Core.Utilities.FieldDeviceIdentifier;
using DataAccess.Abstract;
using Entities.Concrete;
using FieldBusiness.Abstract;
using FieldEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class EventArchiveParameterManager : IEventArchiveParameterService
    {
        IEventArchiveParameterDal _eventArchiveParameterDal;
        public List<List<FieldEventArchiveParameter>> _fieldEventArchiveParameters;



        public EventArchiveParameterManager(IEventArchiveParameterDal eventArchiveParameterDal)
        {
            _eventArchiveParameterDal = eventArchiveParameterDal;
            _fieldEventArchiveParameters = new List<List<FieldEventArchiveParameter>>();
        }



        [WinFormSecuredOperation(MethodAccessCodes.BusinessEventArchiveManagerGetArchiveFromDatabase, Messages.EventArchiveManagerGetArchiveFromDatabase, Priority = 1)]
        [LogAspect(typeof(FileLogger), Priority = 2)]
        public IDataResult<List<EventArchiveParameter>> GetArchiveFromDatabaseByDateTimeInterval(int deviceId,DateTime beginDate, DateTime endDate)
        {
            return new SuccessDataResult<List<EventArchiveParameter>>(_eventArchiveParameterDal.GetAll(ep => ep.HistoryDateTime >= beginDate && ep.HistoryDateTime <= endDate && ep.DeviceId==deviceId));
        }



        [WinFormSecuredOperation(MethodAccessCodes.BusinessEventArchiveManagerGetArchiveFromDevice, Messages.DailyArchiveManagerGetArchiveFromDevice, Priority = 1)]
        [ValidationAspect(typeof(DataTransmissionParametersHolderListValidator), Priority = 2)]
        [LogAspect(typeof(FileLogger), Priority = 3)]
        public async Task<IResult> GetArchivesFromDeviceAsync(DataTransmissionParametersHolderList deviceParameters, IProgress<ProgressStatus> progress)
        {
            _fieldEventArchiveParameters.Clear();
            var semaphoreSlim = ConcurrentTaskLimiter.GetSemaphoreSlim();

            return await Task<IResult>.Run( async() =>
            {
                foreach (var deviceParameter in deviceParameters.DataTransmissionParameterHolderList)
                {
                    deviceParameter.SemaphoreSlimT = semaphoreSlim;
                    await deviceParameter.SemaphoreSlimT.WaitAsync();
                    var fieldEventArchiveParameterService = AutofacBusinessContainerBuilder.AutofacBusinessContainer.Resolve<IFieldEventArchiveParameterService>();
                    var result = fieldEventArchiveParameterService.GetArchiveFromDeviceAsync(deviceParameter);
                    fieldEventArchiveParameterService.OnFieldDataIsReadyEvent += FieldEventArchiveParameterService_OnFieldDataIsReadyEvent;
                }

                return new SuccessResult();
            });
        }



        private void FieldEventArchiveParameterService_OnFieldDataIsReadyEvent(object sender, Core.Events.Results.FieldEventResult<FieldEventArchiveParameter, IProgress<Core.ActionReports.ProgressStatus>> e)
        {
            _fieldEventArchiveParameters.Add(e.DataList);

            using (var scope = AutofacBusinessContainerBuilder.AutofacBusinessContainer.BeginLifetimeScope())
            {
                var eventArchiveParameterManager = scope.Resolve<IEventArchiveParameterService>();
                var result = eventArchiveParameterManager.AddArchiveParameterTransactionOperation(e.DataList,e.Progress);

                if (result == null)
                {
                    ErrorProgressReport(e.Progress, Messages.DatabaseEventArchiveComonError);
                }
            }
        }




        [TransactionScopeAspect(Priority = 1)]
        [LogAspect(typeof(FileLogger), Priority = 2)]
        public IResult AddArchiveParameterTransactionOperation(List<FieldEventArchiveParameter> fieldEventArchiveParameters, IProgress<ProgressStatus> progress)
        {
            using (var scope = AutofacBusinessContainerBuilder.AutofacBusinessContainer.BeginLifetimeScope())
            {
                IEventArchiveParameterDal eventArchiveParameterDal = scope.Resolve<IEventArchiveParameterDal>();
                foreach (var parameter in fieldEventArchiveParameters)
                {
                    var eventArchiveParameter = EventArchiveParameterConverters.ConvertToDatabaseFormat(parameter);
                    var IsCurrentArchiveRowExists = eventArchiveParameterDal.Get(hp => hp.HistoryDateTime == eventArchiveParameter.HistoryDateTime && hp.AbNo == eventArchiveParameter.AbNo && hp.DeviceId==eventArchiveParameter.DeviceId);
                    if (IsCurrentArchiveRowExists != null)
                    {
                        eventArchiveParameterDal.Delete(IsCurrentArchiveRowExists);
                    }
                    eventArchiveParameterDal.Add(eventArchiveParameter);
                }
            }
            return new SuccessResult();
        }




        private void ErrorProgressReport(IProgress<ProgressStatus> progress, string message)
        {
            progress.Report(new ProgressStatus { StatusId = MessageStatus.Error, Message = message });
        }
    }
}
