using Core.Utilities.FieldDeviceIdentifier;
using FieldBusiness.FieldBusinessMessages;
using FluentValidation;

namespace FieldBusiness.ValidationRules.FluentValidation
{
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
