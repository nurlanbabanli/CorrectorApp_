using Core.Utilities.FieldDeviceIdentifier;
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
        }
    }
}
