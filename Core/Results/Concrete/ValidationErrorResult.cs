using Core.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Results.Concrete
{
    public partial class ValidationErrorResult:ValidationResult, IValidationResult
    {

        public ValidationErrorResult(List<string> messageList) : base(messageList, false)
        {

        }

        public ValidationErrorResult() : base(false)
        {

        }

    }
}
