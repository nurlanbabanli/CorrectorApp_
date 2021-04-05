using Business.BusinessMessages;
using Core.Results.Abstract;
using Core.Results.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Helper.RuleChecker
{
    public class UserRules
    {
        public static IResult CheckIfUserIdExist(IUserDal userDal, string userId)
        {
            var result = userDal.GetAll(us => us.UserId.Trim().ToUpper() == userId.Trim().ToUpper()).Any();
            if (result)
            {
                return new ErrorResult(Messages.UserIdIsExists);
            }
            return new SuccessResult();
        }
    }
}
