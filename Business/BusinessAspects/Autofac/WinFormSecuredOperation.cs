using Business.BusinessMessages;
using Business.Events;
using Business.Utilities.Security;
using Castle.DynamicProxy;
using Core.Events.Abstract;
using Core.Utilities.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.BusinessAspects.Autofac
{
    public class WinFormSecuredOperation:MethodInterception
    {
        string _methodAccessCode;
        string _modulFunction;
        public WinFormSecuredOperation(string accessCode, string moduleFunction)
        {
            _methodAccessCode = accessCode;
            _modulFunction = moduleFunction;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var currentUserAccess = ActiveUser.userAccess;
            if (currentUserAccess != null)
            {
                foreach (var access in currentUserAccess)
                {
                    if (_methodAccessCode == access.AccessCode && access.UserId == ActiveUser.activeUser.UserId)
                    {
                        return;
                    }
                }
                throw new Exception( ActiveUser.activeUser.UserId+" "+_modulFunction);
            }
            throw new Exception(Messages.ActiveUserNotFound);
        }
    }
}
