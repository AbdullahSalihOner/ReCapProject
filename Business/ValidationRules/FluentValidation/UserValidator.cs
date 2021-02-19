using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator: AbstractValidator<User>
    {
        public UserValidator()
        {

            RuleFor(u => u.FirstName).MinimumLength(2);
            RuleFor(u => u.LastName).NotEmpty();
            RuleFor(u => u.Email).NotEmpty();
            RuleFor(u => u.Email).EmailAddress();
            RuleFor(u => u.Password).MinimumLength(4).WithMessage(" password must be at least 6 characters.");
            RuleFor(u => u.Password).Matches("[A-Z]").WithMessage("Your password must contain at least one uppercase letter.");
            RuleFor(u => u.Password).Matches("[a-z]").WithMessage("Your password must contain at least one lowercase letter.");
            RuleFor(u => u.Password).Matches("[0-9]").WithMessage("Your password must contain at least one number.");
            //Şifre ile ilgili kısıtylamlar ekle
        }
    }
}
