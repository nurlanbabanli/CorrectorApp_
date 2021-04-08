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
    public class MonthlyType2ArchiveParameterManager : IMonthlyType2ArchiveParameterService
    {
        IMonthlyType2ArchiveParameterDal _monthlyType2ArchiveParameterDal;
        public List<List<FieldMonthlyType2ArchiveParameter>> _fieldMonthlyType2ArchiveParameters;


        public MonthlyType2ArchiveParameterManager(IMonthlyType2ArchiveParameterDal monthlyType2ArchiveParameterDal)
        {
            _monthlyType2ArchiveParameterDal = monthlyType2ArchiveParameterDal;
            _fieldMonthlyType2ArchiveParameters = new List<List<FieldMonthlyType2ArchiveParameter>>();
        }



        [WinFormSecuredOperation(MethodAccessCodes.BusinessMonthlyType2ArchiveManagerGetArchiveFromDatabase, Messages.MonthlyType2ArchiveManagerGetArchiveFromDatabase, Priority = 1)]
        [LogAspect(typeof(FileLogger), Priority = 2)]
        public IDataResult<List<MonthlyType2ArchiveParameter>> GetArchiveFromDatabaseByDateTimeInterval(int deviceId, DateTime beginDate, DateTime endDate)
        {
            return new SuccessDataResult<List<MonthlyType2ArchiveParameter>>(_monthlyType2ArchiveParameterDal.GetAll(mp => mp.HistoryDateTime >= beginDate && mp.HistoryDateTime <= endDate && mp.DeviceId == deviceId));
        }



        [WinFormSecuredOperation(MethodAccessCodes.BusinessMonthlyType2ArchiveManagerGetArchiveFromDevice, Messages.MonthlyType2ArchiveManagerGetArchiveFromDevice, Priority = 1)]
        [ValidationAspect(typeof(DataTransmissionParametersHolderListValidator), Priority = 1)]
        [LogAspect(typeof(FileLogger), Priority = 2)]
        public async Task<IResult> GetArchivesFromDeviceAsync(DataTransmissionParametersHolderList deviceParameters, IProgress<ProgressStatus> progress)
        {
            _fieldMonthlyType2ArchiveParameters.Clear();
            var semaphoreSlim = ConcurrentTaskLimiter.GetSemaphoreSlim();

            return await Task<IResult>.Run(async () =>
            {
                foreach (var deviceParameter in deviceParameters.DataTransmissionParameterHolderList)
                {
                    deviceParameter.SemaphoreSlimT = semaphoreSlim;
                    await deviceParameter.SemaphoreSlimT.WaitAsync();
                    var fieldMonthlyType2ArchiveParameterService = AutofacBusinessContainerBuilder.AutofacBusinessContainer.Resolve<IFieldMonthlyType2ArchiveParameterService>();
                    var result = fieldMonthlyType2ArchiveParameterService.GetArchiveFromDeviceAsync(deviceParameter);
                    fieldMonthlyType2ArchiveParameterService.OnFieldDataIsReadyEvent += FieldMonthlyType2ArchiveParameterService_OnFieldDataIsReadyEvent;
                }
                return new SuccessResult();
            });
        }



        private void FieldMonthlyType2ArchiveParameterService_OnFieldDataIsReadyEvent(object sender, Core.Events.Results.FieldEventResult<FieldMonthlyType2ArchiveParameter, IProgress<ProgressStatus>> e)
        {
            _fieldMonthlyType2ArchiveParameters.Add(e.DataList);

            using (var scope = AutofacBusinessContainerBuilder.AutofacBusinessContainer.BeginLifetimeScope())
            {
                var monthlyType2ArchiveParameterManager = scope.Resolve<IMonthlyType2ArchiveParameterService>();
                var result = monthlyType2ArchiveParameterManager.AddArchiveParameterTransactionOperation(e.DataList,e.Progress);

                if (result == null)
                {
                    ErrorProgressReport(e.Progress, Messages.DatabaseMonthlyType2ArchiveComonError);
                }
            }
        }



        [TransactionScopeAspect(Priority = 1)]
        [LogAspect(typeof(FileLogger), Priority = 2)]
        public IResult AddArchiveParameterTransactionOperation(List<FieldMonthlyType2ArchiveParameter> fieldMonthlyType2ArchiveParameters, IProgress<ProgressStatus> progress)
        {
            using (var scope = AutofacBusinessContainerBuilder.AutofacBusinessContainer.BeginLifetimeScope())
            {
                IMonthlyType2ArchiveParameterDal monthlyType2ArchiveParameterDal = scope.Resolve<IMonthlyType2ArchiveParameterDal>();
                foreach (var parameter in fieldMonthlyType2ArchiveParameters)
                {
                    var monthlyType2ArchiveParameter = MonthlyType2ArchiveParameterConverters.ConvertToDatabaseFormat(parameter);
                    var IsCurrentArchiveRowExists = monthlyType2ArchiveParameterDal.Get(dp => dp.HistoryDateTime == monthlyType2ArchiveParameter.HistoryDateTime && dp.AbNo == monthlyType2ArchiveParameter.AbNo && dp.DeviceId == monthlyType2ArchiveParameter.DeviceId);
                    if (IsCurrentArchiveRowExists != null)
                    {
                        monthlyType2ArchiveParameterDal.Delete(IsCurrentArchiveRowExists);
                    }
                    monthlyType2ArchiveParameterDal.Add(monthlyType2ArchiveParameter);
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
