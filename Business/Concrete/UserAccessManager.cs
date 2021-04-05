using Business.Abstract;
using Business.Helper.RuleChecker;
using Business.Utilities;
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
    public class UserAccessManager : IUserAccessService
    {
        IUserAccessDal _userAccessDal;

        public UserAccessManager(IUserAccessDal userAccessDal)
        {
            _userAccessDal = userAccessDal;
        }

        public IResult Add(UserAccess userAccess)
        {
            IResult result = BusinessRules.Run(UserAccessRules.CheckIfUserAccessExist(_userAccessDal, userAccess.UserId, userAccess.AccessCode));
            if (result!=null)
            {
                return result;
            }
            _userAccessDal.Add(userAccess);
            return new SuccessResult();
        }

        public IDataResult<List<UserAccess>> GetByUserId(string userId)
        {
            return new SuccessDataResult<List<UserAccess>>(_userAccessDal.GetAll(usa=>usa.UserId==userId));
        }

        public IResult Delete(UserAccess userAccess)
        {
            IResult result = BusinessRules.Run(UserAccessRules.CheckIfUserAccessExist(_userAccessDal, userAccess.UserId, userAccess.AccessCode));
            if (result!=null)
            {
                _userAccessDal.Delete(userAccess);
            }
            return new SuccessResult();
        }
    }
}
