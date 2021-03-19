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
    public class DataTransmissionParametersHolderListValidator : AbstractValidator<DataTransmissionParametersHolderList>
    {
        public DataTransmissionParametersHolderListValidator()
        {
            RuleFor(list => list.DataTransmissionParameterHolderList.Count).GreaterThan(0).WithMessage(BusinessMessages.Messages.DeviceParametersIsEmpty);
        }

    }
}
