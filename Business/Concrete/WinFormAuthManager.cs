using Business.Abstract;
using Business.BusinessMessages;
using Business.Utilities.Security;
using Business.ValidationRules.FluentValidation;
using Core.ActionReports;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Results.Abstract;
using Core.Results.Concrete;
using Core.Tools;
using Core.Utilities.Security;
using Core.Utilities.Security.Hashing;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class WinFormAuthManager : IWinFormAuthService
    {
        IUserService _userService;
        IUserAccessService _userAccessService;
        IUserLogService _userLogService;

        public WinFormAuthManager(IUserService userService, IUserAccessService userAccessService,IUserLogService userLogService)
        {
            _userService = userService;
            _userAccessService = userAccessService;
            _userLogService = userLogService;
        }

        [LogAspect(typeof(FileLogger), Priority = 2)]
        public IDataResult<User> Login(UserForLoginDto userForLoginDto, IProgress<ProgressStatus> progress)
        {
            var userToCheck = _userService.GetById(userForLoginDto.UserId).Data;
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(null,Messages.UserNotFound);
            }
            var passwordHolder = new PasswordHolder { PasswordHash = userToCheck.PasswordHash, PasswordSalt = userToCheck.PasswordSalt };
            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, passwordHolder))
            {
                return new ErrorDataResult<User>(null,Messages.PasswordError);
            }

            GetCurentUserAccess(userToCheck);
            UserLoginLog(userToCheck,progress);
            return new SuccessDataResult<User>(userToCheck, Messages.SuccessfullLogin);
        }

        public IResult Logout(IProgress<ProgressStatus> progress)
        {
            UserLogoutLog(progress);
            ActiveUser.activeUser = null;
            ActiveUser.userAccess = null;
            ActiveUser.UserLoginDate = null;
            return new SuccessResult();
        }

        [ValidationAspect(typeof(UserForRegisterDtoValidator), Priority = 1)]
        public IDataResult<User> RegisterUserWithAccess(UserForRegisterDto userForRegisterDto, IProgress<ProgressStatus> progress, List<UserAccess> userAccess)
        {
            var passwordHolder = HashingHelper.CreatePasswordHash(userForRegisterDto.Password);
            var user = new User
            {
                UserId = userForRegisterDto.UserId,
                PasswordHash = passwordHolder.PasswordHash,
                PasswordSalt = passwordHolder.PasswordSalt,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                Email = userForRegisterDto.Email,
                IsAdmin = userForRegisterDto.IsAdmin,
                Status = userForRegisterDto.Status,
                DepartmentName = userForRegisterDto.DepartmentName,
                HostIp = userForRegisterDto.HostIp,
                Position = userForRegisterDto.Position
            };

            IResult result = _userService.AddWithAccess(user, userAccess, progress);
            if (result == null)
            {
                return new ErrorDataResult<User>(user, Messages.UserRegistrationFaild);
            }
            else if (!result.IsSuccess)
            {
                return new ErrorDataResult<User>(user, result.Message);
            }

            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        [ValidationAspect(typeof(UserForRegisterDtoValidator), Priority = 1)]
        public IDataResult<User> RegisterUser(UserForRegisterDto userForRegisterDto, IProgress<ProgressStatus> progress)
        {
            var passwordHolder = HashingHelper.CreatePasswordHash(userForRegisterDto.Password);
            var user = new User
            {
                UserId = userForRegisterDto.UserId,
                PasswordHash = passwordHolder.PasswordHash,
                PasswordSalt = passwordHolder.PasswordSalt,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                Email = userForRegisterDto.Email,
                IsAdmin = userForRegisterDto.IsAdmin,
                Status = userForRegisterDto.Status,
                DepartmentName = userForRegisterDto.DepartmentName,
                HostIp = userForRegisterDto.HostIp,
                Position = userForRegisterDto.Position
            };

            IResult result = _userService.Add(user, progress);
            if (result==null)
            {
                return new ErrorDataResult<User>(user, Messages.UserRegistrationFaild);
            }
            else if(!result.IsSuccess)
            {
                return new ErrorDataResult<User>(user, result.Message);
            }

            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        private void GetCurentUserAccess(User user)
        {
            ActiveUser.activeUser = user;
            ActiveUser.UserLoginDate = FormatDateTime.FormatDateTimeAsDateTime(DateTime.Now);
            ActiveUser.userAccess = _userAccessService.GetByUserId(user.UserId).Data;
        }

        private void UserLoginLog(User user, IProgress<ProgressStatus> progress)
        {           
            var userLog = new UserLog
            {
                UserId = user.UserId,
                HostName = System.Net.Dns.GetHostName(),
                UserName = Environment.UserName,
                LoginDate = ActiveUser.UserLoginDate,
            };
            _userLogService.Add(userLog, progress);          
        }

        private void UserLogoutLog(IProgress<ProgressStatus> progress)
        {
            if (ActiveUser.activeUser!=null)
            {
                var userLog = _userLogService.Get(ActiveUser.activeUser.UserId, ActiveUser.UserLoginDate).Data;
                userLog.LogoutDate = FormatDateTime.FormatDateTimeAsDateTime(DateTime.Now);
                IResult result= _userLogService.Update(userLog, progress);
            }
        }
    }
}
