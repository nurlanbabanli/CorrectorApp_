using Business.BusinessMessages;
using Core.Utilities.FieldDeviceIdentifier;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class DeviceParameterValidator:AbstractValidator<DataTransmissionParameterHolder>
    {
        public DeviceParameterValidator()
        {
            RuleFor(dp => dp.DeviceParametersHolder.Id).NotNull();
            RuleFor(dp => dp.SemaphoreSlimT).NotNull();
            RuleFor(dp => dp.DeviceParametersHolder.IpAddresss).NotNull().WithMessage(Messages.DeviceIpIsNull);
        }
    }
}
