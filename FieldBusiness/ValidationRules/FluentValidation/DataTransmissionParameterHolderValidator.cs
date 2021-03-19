using Core.Utilities.FieldDeviceIdentifier;
using FieldBusiness.FieldBusinessMessages;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldBusiness.ValidationRules.FluentValidation
{
    public class DataTransmissionParameterHolderValidator:AbstractValidator<DataTransmissionParameterHolder>
    {
        public DataTransmissionParameterHolderValidator()
        {
            RuleFor(dt => dt.UserInterfaceParametersHolder).NotNull().WithMessage("Nurlan");
            RuleFor(dt => dt.ArchiveParametersHolder).NotEmpty();
            RuleFor(dt => dt.SemaphoreSlimT).NotNull().WithMessage("nn");
            RuleFor(dt => dt.DeviceParametersHolder).NotNull().WithMessage("bb");
            RuleFor(dt => dt.DeviceParametersHolder).SetValidator(new DeviceParametersHolderValidator());
        }
    }

    public class DeviceParametersHolderValidator:AbstractValidator<DeviceParameters>
    {
        private string k = null;
        public DeviceParametersHolderValidator()
        {
           // RuleFor(d => d.Id).NotEmpty().WithMessage(Messages.DeviceIdIsNull);
            RuleFor(d => d.IpAddresss).NotEmpty().WithMessage(Messages.DeviceIpIsNull);
            
        }
    }
}
