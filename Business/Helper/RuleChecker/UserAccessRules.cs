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
    public class UserAccessRules
    {
        public static IResult CheckIfUserAccessExist(IUserAccessDal userAccessDal, string userId,string accessCode)
        {
            var result = userAccessDal.GetAll(us => us.UserId.Trim().ToUpper() == userId.Trim().ToUpper() && us.AccessCode.Trim().ToUpper()==accessCode.Trim().ToUpper()).Any();
            if (result)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }
    }
}
