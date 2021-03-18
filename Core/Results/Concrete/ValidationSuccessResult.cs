using Core.Results.Abstract;
using System.Collections.Generic;

namespace Core.Results.Concrete
{

    public class ValidationSuccessResult : ValidationResult, IValidationResult
    {
        public ValidationSuccessResult(List<string> messageList) : base(messageList, true)
        {

        }

        public ValidationSuccessResult() : base(true)
        {

        }
    }
}
