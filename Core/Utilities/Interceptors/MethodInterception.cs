using Castle.DynamicProxy;
using Core.Results.Abstract;
using Core.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Interceptors
{
    public abstract class MethodInterception : MethodInterceptionBaseAttribute
    {
        protected virtual IResult OnBefore(IInvocation invocation) { return new SuccessResult(); }
        protected virtual void OnAfter(IInvocation invocation) { }
        protected virtual void OnException(IInvocation invocation, Exception exception) { }
        protected virtual void OnSuccess(IInvocation invocation) { }


        public override void Intercept(IInvocation invocation)
        {
            var isSuccess = true;
            if (OnBefore(invocation).IsSuccess)
            {
                try
                {
                    invocation.Proceed();
                }
                catch (Exception e)
                {
                    isSuccess = false;
                    OnException(invocation, e);
                    throw;
                }
                finally
                {
                    if (isSuccess)
                    {
                        OnSuccess(invocation);
                    }
                }
                OnAfter(invocation);
            }
            else
            {

            }
        }
    }
}
