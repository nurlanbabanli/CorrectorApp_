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
            //RuleFor(dt => dt.UserInterfaceParametersHolder).NotNull().WithMessage("Nurlan");
           // RuleFor(dt => dt.ArchiveParametersHolder).NotEmpty();
            RuleFor(dt => dt.SemaphoreSlimT).NotNull().WithMessage(Messages.SemaphoreSlimTIsNull);
            RuleFor(dt => dt.DeviceParametersHolder).NotNull().WithMessage(Messages.DeviceParametersHolderIsNull);
            RuleFor(dt => dt.DeviceParametersHolder).SetValidator(new DeviceParametersHolderValidator());
        }
    }

    public class DeviceParametersHolderValidator:AbstractValidator<DeviceParameters>
    {
        public DeviceParametersHolderValidator()
        {
            RuleFor(d => d.Id).NotEmpty().WithMessage(Messages.DeviceIdIsNull);
            RuleFor(d => d.IpAddresss).NotEmpty().WithMessage(m=>$"{Messages.DeviceIpIsNull+"/DeviceId="+m.Id.ToString()}");
            RuleFor(d => d.DeviceType).NotEmpty().WithMessage(m => $"{Messages.DeviceTypeIsNull + "/DeviceId=" + m.Id.ToString()}");
            RuleFor(d => d.IsActive).Must(x => x == true).WithMessage(m => $"{Messages.DeviceIsNotActive + "/DeviceId=" + m.Id.ToString()}");
        }
    }
}
