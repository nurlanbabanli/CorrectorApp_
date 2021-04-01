using Autofac;
using Business.Abstract;
using Business.BusinessMessages;
using Business.DependencyResolvers.Autofac;
using Business.Helper;
using Business.Helper.Logging;
using Business.Utilities;
using Business.ValidationRules.FluentValidation;
using Core.ActionReports;
using Core.Aspects.Autofac.Exception;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.FluentValidation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Events.Results;
using Core.Results.Abstract;
using Core.Results.Concrete;
using Core.Utilities.FieldDeviceIdentifier;
using Core.Utilities.InMemoryLoggerParameters;
using DataAccess.Abstract;
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
        public List<List<FieldHourlyArchiveParameter>> _fieldHourlyArchiveParameters;
        IHourlyArchiveParameterDal _hourlyArchiveParameterDal;

        public HourlyArchiveParameterManager(IHourlyArchiveParameterDal hourlyArchiveParameterDal)
        {
            _hourlyArchiveParameterDal = hourlyArchiveParameterDal;
            _fieldHourlyArchiveParameters = new List<List<FieldHourlyArchiveParameter>>();
            
        }

        public IDataResult<List<HourlyArchiveParameter>> GetArchiveFromDatabaseByDateTimeInterval(DateTime beginDate, DateTime endDate)
        {
            return new SuccessDataResult<List<HourlyArchiveParameter>>(_hourlyArchiveParameterDal.GetAll(d => d.HistoryDateTime >= beginDate && d.HistoryDateTime <= endDate));
        }


        [ValidationAspect(typeof(DataTransmissionParametersHolderListValidator), Priority = 1)]
        [LogAspect(typeof(FileLogger), Priority = 2)]
        public async Task GetArchivesFromDeviceAsync(DataTransmissionParametersHolderList deviceParameters)
        {
            InMemoryLoggedMessages.InMemoryHourMesssageLoggerParameters.Clear();
            _fieldHourlyArchiveParameters.Clear();
            var semaphoreSlim = ConcurrentTaskLimiter.GetSemaphoreSlim();
            foreach (var deviceParameter in deviceParameters.DataTransmissionParameterHolderList)
            {
                //throw new Exception("Nurlan");
                deviceParameter.SemaphoreSlimT = semaphoreSlim;
                await deviceParameter.SemaphoreSlimT.WaitAsync();
                var fieldHourArchiveParameterService = AutofacBusinessContainerBuilder.AutofacBusinessContainer.Resolve<IFieldHourArchiveParameterService>();
                var result = fieldHourArchiveParameterService.GetHourArchiveFromDeviceAsync(deviceParameter);
                fieldHourArchiveParameterService.OnFieldDataIsReadyEvent += FieldHourArchiveParameterService_OnFieldDataIsReadyEvent;

                if (result == null)
                {
                    ErrorProgressReport(deviceParameter.UserInterfaceParametersHolder.ProgressReport,
                     MessageFormatter.GetMessage(InMemoryLoggedMessages.InMemoryHourMesssageLoggerParameters, deviceParameter.DeviceParametersHolder.Id));
                }
            }
        }

        private void FieldHourArchiveParameterService_OnFieldDataIsReadyEvent(object sender, FieldEventResult<FieldHourlyArchiveParameter, IProgress<ProgressStatus>> e)
        {
            _fieldHourlyArchiveParameters.Add(e.DataList);

            using (var scope = AutofacBusinessContainerBuilder.AutofacBusinessContainer.BeginLifetimeScope())
            {
                var hourlyArchiveParameterManager = scope.Resolve<IHourlyArchiveParameterService>();
                var result = hourlyArchiveParameterManager.AddArchiveParameterTransactionOperation(e.DataList);

                if (result == null)
                {
                    ErrorProgressReport(e.Progress, Messages.DatabaseHourArchiveComonError);     
                }
            }
        }
      
        [TransactionScopeAspect(Priority = 1)]
        [LogAspect(typeof(FileLogger), Priority = 2)]
        public IResult AddArchiveParameterTransactionOperation(List<FieldHourlyArchiveParameter> fieldhourlyArchiveParameter)
        {
            using (var scope = AutofacBusinessContainerBuilder.AutofacBusinessContainer.BeginLifetimeScope())
            {
                IHourlyArchiveParameterDal hourlyArchiveParameterDal = scope.Resolve<IHourlyArchiveParameterDal>();
                foreach (var parameter in fieldhourlyArchiveParameter)
                {
                    var hourlyArchiveParameter = HourlyArchiveParameterConverters.ConvertToDatabaseFormat(parameter);
                    var IsCurrentArchiveRowExists = hourlyArchiveParameterDal.Get(hp => hp.HistoryDateTime == hourlyArchiveParameter.HistoryDateTime && hp.AbNo == hourlyArchiveParameter.AbNo);
                    if (IsCurrentArchiveRowExists!=null)
                    {
                        hourlyArchiveParameterDal.Delete(IsCurrentArchiveRowExists);
                    }
                    hourlyArchiveParameterDal.Add(hourlyArchiveParameter);
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
