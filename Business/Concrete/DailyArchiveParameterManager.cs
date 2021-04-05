using Autofac;
using Business.Abstract;
using Business.BusinessMessages;
using Business.DependencyResolvers.Autofac;
using Business.Helper.Logging;
using Business.Helper.ParameterConverters;
using Business.Utilities;
using Business.ValidationRules.FluentValidation;
using Core.ActionReports;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Events.Results;
using Core.Results.Abstract;
using Core.Results.Concrete;
using Core.Utilities.FieldDeviceIdentifier;
using Core.Utilities.InMemoryLoggerParameters;
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
    public class DailyArchiveParameterManager : IDailyArchiveParameterService
    {
        IDailyArchiveParameterDal _dailyArchiveParameterDal;
        public List<List<FieldDailyArchiveParameter>> _fieldDailyArchiveParameters;
        public DailyArchiveParameterManager(IDailyArchiveParameterDal dailyArchiveParameterDal)
        {
            _dailyArchiveParameterDal = dailyArchiveParameterDal;
        }

        public IDataResult<List<DailyArchiveParameter>> GetArchiveFromDatabaseByDateTimeInterval(int deviceId, DateTime beginDate, DateTime endDate)
        {
            return new SuccessDataResult<List<DailyArchiveParameter>>(_dailyArchiveParameterDal.GetAll(dp => dp.HistoryDateTime >= beginDate && dp.HistoryDateTime <= endDate && dp.DeviceId == deviceId));
        }

        [ValidationAspect(typeof(DataTransmissionParametersHolderListValidator), Priority = 1)]
        [LogAspect(typeof(FileLogger), Priority = 2)]
        public async Task GetArchivesFromDeviceAsync(DataTransmissionParametersHolderList deviceParameters)
        {
            InMemoryLoggedMessages.InMemoryMesssageLoggerParameters.Clear();
            _fieldDailyArchiveParameters.Clear();
            var semaphoreSlim = ConcurrentTaskLimiter.GetSemaphoreSlim();

            await Task.Run(async () =>
            {
                foreach (var deviceParameter in deviceParameters.DataTransmissionParameterHolderList)
                {
                    deviceParameter.SemaphoreSlimT = semaphoreSlim;
                    await deviceParameter.SemaphoreSlimT.WaitAsync();
                    var fieldDailyArchiveParameterService = AutofacBusinessContainerBuilder.AutofacBusinessContainer.Resolve<IFieldDailyArchiveParameterService>();
                    var result = fieldDailyArchiveParameterService.GetDailyArchiveFromDeviceAsync(deviceParameter);
                    fieldDailyArchiveParameterService.OnFieldDataIsReadyEvent += FieldDailyArchiveParameterService_OnFieldDataIsReadyEvent;

                    if (result == null)
                    {
                        ErrorProgressReport(deviceParameter.UserInterfaceParametersHolder.ProgressReport,
                         MessageFormatter.GetMessage(InMemoryLoggedMessages.InMemoryMesssageLoggerParameters, deviceParameter.DeviceParametersHolder.Id));
                    }
                }
            });
        }

        private void FieldDailyArchiveParameterService_OnFieldDataIsReadyEvent(object sender, FieldEventResult<FieldDailyArchiveParameter, IProgress<ProgressStatus>> e)
        {
            _fieldDailyArchiveParameters.Add(e.DataList);

            using (var scope = AutofacBusinessContainerBuilder.AutofacBusinessContainer.BeginLifetimeScope())
            {
                var dailyArchiveParameterManager = scope.Resolve<IDailyArchiveParameterService>();
                var result = dailyArchiveParameterManager.AddArchiveParameterTransactionOperation(e.DataList);

                if (result == null)
                {
                    ErrorProgressReport(e.Progress, Messages.DatabaseDailyArchiveComonError);
                }
            }
        }

        [TransactionScopeAspect(Priority = 1)]
        [LogAspect(typeof(FileLogger), Priority = 2)]
        public IResult AddArchiveParameterTransactionOperation(List<FieldDailyArchiveParameter> fieldDailyArchiveParameters)
        {
            using (var scope = AutofacBusinessContainerBuilder.AutofacBusinessContainer.BeginLifetimeScope())
            {
                IDailyArchiveParameterDal dailyArchiveParameterDal = scope.Resolve<IDailyArchiveParameterDal>();
                foreach (var parameter in fieldDailyArchiveParameters)
                {
                    var dailyArchiveParameter = DailyArchiveParameterConverters.ConvertToDatabaseFormat(parameter);
                    var IsCurrentArchiveRowExists = dailyArchiveParameterDal.Get(dp => dp.HistoryDateTime == dailyArchiveParameter.HistoryDateTime && dp.AbNo == dailyArchiveParameter.AbNo && dp.DeviceId==dailyArchiveParameter.DeviceId);
                    if (IsCurrentArchiveRowExists != null)
                    {
                        dailyArchiveParameterDal.Delete(IsCurrentArchiveRowExists);
                    }
                    dailyArchiveParameterDal.Add(dailyArchiveParameter);
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
