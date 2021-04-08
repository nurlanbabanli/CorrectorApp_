using Core.ActionReports;
using Core.Results.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IWinFormAuthService
    {
        IDataResult<User> Login(UserForLoginDto userForLoginDto,IProgress<ProgressStatus> progress);
        IResult Logout(IProgress<ProgressStatus> progress);
        IDataResult<User> RegisterUserWithAccess(UserForRegisterDto userForRegisterDto, IProgress<ProgressStatus> progress, List<UserAccess> userAccess);
        IDataResult<User> RegisterUser(UserForRegisterDto userForRegisterDto, IProgress<ProgressStatus> progress);
    }
}
