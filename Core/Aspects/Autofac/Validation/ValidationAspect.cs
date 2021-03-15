using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.FluentValidation;
using Core.Results.Abstract;
using Core.Results.Concrete;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("Bu bir dogrulama sinifi diyil");
            }

            _validatorType = validatorType;
        }
        protected override IResult OnBefore(IInvocation invocation)
        {
            bool isSuccess = true;
            List<string> errorMessages = new List<string>();

            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
            foreach (var entity in entities)
            {
               IResult result= ValidationTool.Validate(validator, entity);
                if (!result.IsSuccess) { isSuccess = false; errorMessages = result.MessageList; break; }
            }

            if (isSuccess)
                return new SuccessResult();
            else
                return new ErrorResult(errorMessages);
        }
    }
}
