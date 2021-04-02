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
    public class ArchiveDataTransmissionParameterHolderValidator:AbstractValidator<DataTransmissionParameterHolder>
    {
        public ArchiveDataTransmissionParameterHolderValidator()
        {
            //RuleFor(dt => dt.UserInterfaceParametersHolder).NotNull().WithMessage("Nurlan");
           // RuleFor(dt => dt.ArchiveParametersHolder).NotEmpty();
            RuleFor(dt => dt.SemaphoreSlimT).NotNull().WithMessage(Messages.SemaphoreSlimTIsNull);
            RuleFor(dt => dt.DeviceParametersHolder).NotNull().WithMessage(Messages.DeviceParametersHolderIsNull);
            RuleFor(dt => dt.DeviceParametersHolder).SetValidator(new DeviceParametersHolderValidator());
        }
    }
}
