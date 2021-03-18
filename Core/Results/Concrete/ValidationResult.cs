using Core.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Results.Concrete
{
    public class ValidationResult:IValidationResult
    {
        public ValidationResult(List<string> messageList, bool isSuccess) : this(isSuccess)
        {
            MessageList = messageList;
        }

        public ValidationResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
        public bool IsSuccess { get; }
        public List<string> MessageList { get; }
    }
}
