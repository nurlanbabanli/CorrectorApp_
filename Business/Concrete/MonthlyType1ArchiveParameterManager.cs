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
    public class MonthlyType1ArchiveParameterManager : IMonthlyType1ArchiveParameterService
    {
        IMonthlyType1ArchiveParameterDal _monthlyType1ArchiveParameterDal;
        public List<List<FieldMonthlyType1ArchiveParameter>> _fieldMonthlyType1ArchiveParameters;

        public MonthlyType1ArchiveParameterManager(IMonthlyType1ArchiveParameterDal monthlyType1ArchiveParameterDal)
        {
            _monthlyType1ArchiveParameterDal = monthlyType1ArchiveParameterDal;
        }
       
        public IDataResult<List<MonthlyType1ArchiveParameter>> GetArchiveFromDatabaseByDateTimeInterval(int deviceId, DateTime beginDate, DateTime endDate)
        {
            return new SuccessDataResult<List<MonthlyType1ArchiveParameter>>(_monthlyType1ArchiveParameterDal.GetAll(mp => mp.HistoryDateTime >= beginDate && mp.HistoryDateTime <= endDate && mp.DeviceId == deviceId));
        }


        [ValidationAspect(typeof(DataTransmissionParametersHolderListValidator), Priority = 1)]
        [LogAspect(typeof(FileLogger), Priority = 2)]
        public async Task GetArchivesFromDeviceAsync(DataTransmissionParametersHolderList deviceParameters)
        {
            InMemoryLoggedMessages.InMemoryMesssageLoggerParameters.Clear();
            _fieldMonthlyType1ArchiveParameters.Clear();
            var semaphoreSlim = ConcurrentTaskLimiter.GetSemaphoreSlim();
            foreach (var deviceParameter in deviceParameters.DataTransmissionParameterHolderList)
            {
                deviceParameter.SemaphoreSlimT = semaphoreSlim;
                await deviceParameter.SemaphoreSlimT.WaitAsync();
                var fieldMonthlyType1ArchiveParameterService = AutofacBusinessContainerBuilder.AutofacBusinessContainer.Resolve<IFieldMonthlyType1ArchiveParameterService>();
                var result = fieldMonthlyType1ArchiveParameterService.GetMonthlyType1ArchiveFromDeviceAsync(deviceParameter);
                fieldMonthlyType1ArchiveParameterService.OnFieldDataIsReadyEvent += FieldMonthlyType1ArchiveParameterService_OnFieldDataIsReadyEvent;

                if (result == null)
                {
                    ErrorProgressReport(deviceParameter.UserInterfaceParametersHolder.ProgressReport,
                     MessageFormatter.GetMessage(InMemoryLoggedMessages.InMemoryMesssageLoggerParameters, deviceParameter.DeviceParametersHolder.Id));
                }
            }
        }

        private void FieldMonthlyType1ArchiveParameterService_OnFieldDataIsReadyEvent(object sender, FieldEventResult<FieldMonthlyType1ArchiveParameter, IProgress<ProgressStatus>> e)
        {
            _fieldMonthlyType1ArchiveParameters.Add(e.DataList);

            using (var scope = AutofacBusinessContainerBuilder.AutofacBusinessContainer.BeginLifetimeScope())
            {
                var monthlyType1ArchiveParameterManager = scope.Resolve<IMonthlyType1ArchiveParameterService>();
                var result = monthlyType1ArchiveParameterManager.AddArchiveParameterTransactionOperation(e.DataList);

                if (result == null)
                {
                    ErrorProgressReport(e.Progress, Messages.DatabaseMonthlyType1ArchiveComonError);
                }
            }
        }

        [TransactionScopeAspect(Priority = 1)]
        [LogAspect(typeof(FileLogger), Priority = 2)]
        public IResult AddArchiveParameterTransactionOperation(List<FieldMonthlyType1ArchiveParameter> fieldMonthlyType1ArchiveParameters)
        {
            using (var scope = AutofacBusinessContainerBuilder.AutofacBusinessContainer.BeginLifetimeScope())
            {
                IMonthlyType1ArchiveParameterDal monthlyType1ArchiveParameterDal = scope.Resolve<IMonthlyType1ArchiveParameterDal>();
                foreach (var parameter in fieldMonthlyType1ArchiveParameters)
                {
                    var monthlyType1ArchiveParameter = MonthlyType1ArchiveParameterConverters.ConvertToDatabaseFormat(parameter);
                    var IsCurrentArchiveRowExists = monthlyType1ArchiveParameterDal.Get(dp => dp.HistoryDateTime == monthlyType1ArchiveParameter.HistoryDateTime && dp.AbNo == monthlyType1ArchiveParameter.AbNo && dp.DeviceId == monthlyType1ArchiveParameter.DeviceId);
                    if (IsCurrentArchiveRowExists != null)
                    {
                        monthlyType1ArchiveParameterDal.Delete(IsCurrentArchiveRowExists);
                    }
                    monthlyType1ArchiveParameterDal.Add(monthlyType1ArchiveParameter);
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
