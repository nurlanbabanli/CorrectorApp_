using Core.Results.Abstract;
using Core.Results.Concrete;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.FluentValidation
{
    public static class ValidationTool
    {
        public static IResult Validate(IValidator validator, object entity)
        {
            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);
            if (result.IsValid)
                return new SuccessResult();
            else
                return new ErrorResult(GetErrorMessages(result.Errors));
        }


        private static List<string> GetErrorMessages(IList<ValidationFailure> errors)
        {
            List<string> ErrorMessages = new List<string>();
            foreach (var error in errors)
            {
                ErrorMessages.Add(error.ErrorMessage);
            }
            return ErrorMessages;
        }
    }
}
