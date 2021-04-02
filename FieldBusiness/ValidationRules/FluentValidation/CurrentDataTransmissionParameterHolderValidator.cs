using Core.Utilities.FieldDeviceIdentifier;
using FieldBusiness.FieldBusinessMessages;
using FluentValidation;

namespace FieldBusiness.ValidationRules.FluentValidation
{
    public class CurrentDataTransmissionParameterHolderValidator : AbstractValidator<DataTransmissionParameterHolder>
    {
        public CurrentDataTransmissionParameterHolderValidator()
        {
            //RuleFor(dt => dt.UserInterfaceParametersHolder).NotNull().WithMessage("Nurlan");
            RuleFor(dt => dt.SemaphoreSlimT).NotNull().WithMessage(Messages.SemaphoreSlimTIsNull);
            RuleFor(dt => dt.DeviceParametersHolder).NotNull().WithMessage(Messages.DeviceParametersHolderIsNull);
            RuleFor(dt => dt.DeviceParametersHolder).SetValidator(new DeviceParametersHolderValidator());
        }
    }
}
