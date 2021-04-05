using Business.BusinessMessages;
using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    class UserForRegisterDtoValidator:AbstractValidator<UserForRegisterDto>
    {
        public UserForRegisterDtoValidator()
        {
            RuleFor(u => u.UserId).NotEmpty().WithMessage(Messages.UserIdIsNull);
            RuleFor(u => u.Password).NotEmpty().WithMessage(Messages.PassworIsNull);
        }
    }
}
