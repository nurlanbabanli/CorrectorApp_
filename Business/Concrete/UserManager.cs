using Business.Abstract;
using Business.BusinessMessages;
using Business.Helper.RuleChecker;
using Business.Utilities;
using Core.ActionReports;
using Core.Aspects.Autofac.Transaction;
using Core.Results.Abstract;
using Core.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;
        IUserAccessDal _userAccessDal;

        public UserManager(IUserDal userDal,IUserAccessDal userAccessDal)
        {
            _userDal = userDal;
            _userAccessDal = userAccessDal;
        }

        public IResult Add(User user, IProgress<ProgressStatus> progress)
        {
            IResult result = BusinessRules.Run(UserRules.CheckIfUserIdExist(_userDal, user.UserId));
            if (result!=null)
            {
                return result;
            }
            _userDal.Add(user);
            return new SuccessResult();
        }
        [TransactionScopeAspect(Priority =1)]
        public IResult AddWithAccess(User user, List<UserAccess> userAccess, IProgress<ProgressStatus> progress)
        {
            IResult result = BusinessRules.Run(UserRules.CheckIfUserIdExist(_userDal, user.UserId));
            if (result!=null)
            {
                return result;
            }
            _userDal.Add(user);
            foreach (var access in userAccess)
            {
                IResult accesssResult = BusinessRules.Run(UserAccessRules.CheckIfUserAccessExist(_userAccessDal, access.UserId, access.AccessCode));
                if (accesssResult == null)
                {
                    _userAccessDal.Add(access);
                }              
            }
            return new SuccessResult();
        }

        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll());
        }

        public IDataResult<User> GetById(string userId)
        {
            return new SuccessDataResult<User>(_userDal.Get(us => us.UserId == userId));
        }

        public IResult Update(User user, IProgress<ProgressStatus> progress)
        {
            _userDal.Update(user);
            return new SuccessResult(Messages.UserUpdated);
        }
    }
}
