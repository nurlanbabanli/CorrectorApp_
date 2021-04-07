using Core.ActionReports;
using Core.Results.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserLogService
    {
        IResult Update(UserLog userLog, IProgress<ProgressStatus> progress);
        IResult Add(UserLog userLog, IProgress<ProgressStatus> progress);
        IDataResult<UserLog> Get(string userId, DateTime? loginDate);
    }
}
