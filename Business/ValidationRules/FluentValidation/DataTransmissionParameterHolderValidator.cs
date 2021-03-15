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
    public class DataTransmissionParameterHolderValidator : AbstractValidator<List<DataTransmissionParameterHolder>>
    {
        public DataTransmissionParameterHolderValidator()
        {
            RuleFor(dt => dt.Count).GreaterThan(6).WithMessage("Nurlan");
        }
    }
}
