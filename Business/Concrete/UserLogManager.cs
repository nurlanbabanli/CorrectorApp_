using Business.Abstract;
using Core.ActionReports;
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
    public class UserLogManager : IUserLogService
    {
        IUserLogDal _userLogDal;

        public UserLogManager(IUserLogDal userLogDal)
        {
            _userLogDal = userLogDal;
        }

        public IResult Add(UserLog userLog, IProgress<ProgressStatus> progress)
        {
            _userLogDal.Add(userLog);
            return new SuccessResult();
        }

        public IDataResult<UserLog> Get(string userId, DateTime? loginDate)
        {
            return new SuccessDataResult<UserLog>(_userLogDal.Get(ul => ul.UserId == userId && ul.LoginDate == loginDate));
        }

        public IResult Update(UserLog userLog, IProgress<ProgressStatus> progress)
        {
            _userLogDal.Update(userLog);
            return new SuccessResult();
        }
    }
}
