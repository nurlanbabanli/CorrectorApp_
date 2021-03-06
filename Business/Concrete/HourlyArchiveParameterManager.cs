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
using Core.Events.Abstract;
using Core.Events.Results;
using Core.Results.Abstract;
using Core.Results.Concrete;
using Core.Utilities.FieldDeviceIdentifier;
using DataAccess.Abstract;
using Entities.Concrete;
using FieldBusiness.Abstract;
using FieldEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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




        [WinFormSecuredOperation(MethodAccessCodes.BusinessHourlyArchiveManagerGetArchiveFromDatabase, Messages.HourlyArchiveManagerGetArchiveFromDatabase, Priority = 1)]
        [LogAspect(typeof(FileLogger), Priority = 2)]
        public IDataResult<List<HourlyArchiveParameter>> GetArchiveFromDatabaseByDateTimeInterval(int deviceID,DateTime beginDate, DateTime endDate)
        {
            return new SuccessDataResult<List<HourlyArchiveParameter>>(_hourlyArchiveParameterDal.GetAll(hp => hp.HistoryDateTime >= beginDate && hp.HistoryDateTime <= endDate && hp.DeviceId==deviceID));
        }



        [WinFormSecuredOperation(MethodAccessCodes.BusinessHourlyArchiveManagerGetArchiveFromDevice, Messages.HourlyArchiveManagerGetArchiveFromDevice, Priority =1)]
        [ValidationAspect(typeof(DataTransmissionParametersHolderListValidator), Priority = 2)]
        [LogAspect(typeof(FileLogger), Priority = 3)]
        public async Task<IResult> GetArchivesFromDeviceAsync(DataTransmissionParametersHolderList deviceParameters, IProgress<ProgressStatus> progress)
        {
            _fieldHourlyArchiveParameters.Clear();
            var semaphoreSlim = ConcurrentTaskLimiter.GetSemaphoreSlim();
            
            return await Task<IResult>.Run( async() =>
            {
                foreach (var deviceParameter in deviceParameters.DataTransmissionParameterHolderList)
                {
                    //throw new Exception("Nurlan");
                    deviceParameter.SemaphoreSlimT = semaphoreSlim;
                    await deviceParameter.SemaphoreSlimT.WaitAsync();
                    var fieldHourArchiveParameterService = AutofacBusinessContainerBuilder.AutofacBusinessContainer.Resolve<IFieldHourlyArchiveParameterService>();
                    var result = fieldHourArchiveParameterService.GetArchiveFromDeviceAsync(deviceParameter, deviceParameter.UserInterfaceParametersHolder.ProgressReport);
                    fieldHourArchiveParameterService.OnFieldDataIsReadyEvent += FieldHourArchiveParameterService_OnFieldDataIsReadyEvent;
                }
                return new SuccessResult();
            });
        }




        private void FieldHourArchiveParameterService_OnFieldDataIsReadyEvent(object sender, FieldEventResult<FieldHourlyArchiveParameter, IProgress<ProgressStatus>> e)
        {
            _fieldHourlyArchiveParameters.Add(e.DataList);

            using (var scope = AutofacBusinessContainerBuilder.AutofacBusinessContainer.BeginLifetimeScope())
            {
                var hourlyArchiveParameterManager = scope.Resolve<IHourlyArchiveParameterService>();
                var result = hourlyArchiveParameterManager.AddArchiveParameterTransactionOperation(e.DataList, e.Progress);

                if (result == null)
                {
                    ErrorProgressReport(e.Progress, Messages.DatabaseHourArchiveComonError);     
                }
            }
        }



      
        [TransactionScopeAspect(Priority = 1)]
        [LogAspect(typeof(FileLogger), Priority = 2)]
        public IResult AddArchiveParameterTransactionOperation(List<FieldHourlyArchiveParameter> fieldhourlyArchiveParameters, IProgress<ProgressStatus> progress )
        {
            using (var scope = AutofacBusinessContainerBuilder.AutofacBusinessContainer.BeginLifetimeScope())
            {
                IHourlyArchiveParameterDal hourlyArchiveParameterDal = scope.Resolve<IHourlyArchiveParameterDal>();
                foreach (var parameter in fieldhourlyArchiveParameters)
                {
                    var hourlyArchiveParameter = HourlyArchiveParameterConverters.ConvertToDatabaseFormat(parameter);
                    var IsCurrentArchiveRowExists = hourlyArchiveParameterDal.Get(hp => hp.HistoryDateTime == hourlyArchiveParameter.HistoryDateTime && hp.AbNo == hourlyArchiveParameter.AbNo && hp.DeviceId==hourlyArchiveParameter.DeviceId);
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
