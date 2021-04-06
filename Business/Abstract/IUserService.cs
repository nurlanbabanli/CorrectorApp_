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
    public interface IUserService
    {
        IDataResult<List<User>> GetAll();
        IDataResult<User> GetById(string userId);
        IResult Update(User user, IProgress<ProgressStatus> progress);
        IResult Add(User user, IProgress<ProgressStatus> progress);
        IResult AddWithAccess(User user, List<UserAccess> userAccess, IProgress<ProgressStatus> progress);
    }
}
