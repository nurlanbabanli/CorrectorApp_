using Autofac;
using Business.Abstract;
using Business.DependencyResolvers.Autofac;
using Business.Helper.ParameterConverters;
using Business.Utilities;
using Business.ValidationRules.FluentValidation;
using Core.ActionReports;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Events.Results;
using Core.Results.Abstract;
using Core.Results.Concrete;
using Core.Utilities.FieldDeviceIdentifier;
using FieldBusiness.Abstract;
using FieldEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CurrentParameterManager : ICurrentParameterService
    {
        public List<List<FieldCurrentParameter>> _fieldCurrentParameters;

        public event EventHandler<IDataResult<List<CurrentParameterHolder>>> OnCurrentDataIsReadyEvent;

        [ValidationAspect(typeof(DataTransmissionParametersHolderListValidator), Priority = 1)]
        [LogAspect(typeof(FileLogger), Priority = 2)]
        public async Task GetCurrentParameterFromDeviceAsync(DataTransmissionParametersHolderList deviceParameters)
        {
            var semaphoreSlim = ConcurrentTaskLimiter.GetSemaphoreSlim();

            await Task.Run(async () =>
            {
                foreach (var deviceParameter in deviceParameters.DataTransmissionParameterHolderList)
                {
                    deviceParameter.SemaphoreSlimT = semaphoreSlim;
                    await deviceParameter.SemaphoreSlimT.WaitAsync();
                    var fieldCurrentParameterService = AutofacBusinessContainerBuilder.AutofacBusinessContainer.Resolve<IFieldCurrentParameterService>();
                    var result = fieldCurrentParameterService.GetCurrentParametFromDeviceAsync(deviceParameter);
                    fieldCurrentParameterService.OnFieldDataIsReadyEvent += FieldCurrentParameterService_OnFieldDataIsReadyEvent;
                }
            });

        }

        private void FieldCurrentParameterService_OnFieldDataIsReadyEvent(object sender, FieldEventResult<FieldCurrentParameter, IProgress<ProgressStatus>> e)
        {
            _fieldCurrentParameters.Add(e.DataList);
            OnCurrentDataIsReadyEvent.Invoke(this, new SuccessDataResult<List<CurrentParameterHolder>>(CurrentParameterConverters.ConvertToViewFormat(e.DataList)));
        }

        public Task GetCurrentParameterFromMqttBroker(string MqttTopic)
        {
            throw new NotImplementedException();
        }


        private void ErrorProgressReport(IProgress<ProgressStatus> progress, string message)
        {
            progress.Report(new ProgressStatus { StatusId = MessageStatus.Error, Message = message });
        }
    }
}
