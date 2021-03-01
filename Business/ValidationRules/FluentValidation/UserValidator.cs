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

            RuleFor(user => user.FirstName).NotEmpty();
            RuleFor(user => user.LastName).NotEmpty();
            RuleFor(user => user.PasswordHash).NotEmpty();
            RuleFor(user => user.PasswordSalt).NotEmpty();

            RuleFor(user => user.Email).NotEmpty();
            RuleFor(user => user.Email).EmailAddress();
            //RuleFor(u => u.Password).MinimumLength(4).WithMessage(" password must be at least 6 characters.");
            //RuleFor(u => u.Password).Matches("[A-Z]").WithMessage("Your password must contain at least one uppercase letter.");
            //RuleFor(u => u.Password).Matches("[a-z]").WithMessage("Your password must contain at least one lowercase letter.");
            //RuleFor(u => u.Password).Matches("[0-9]").WithMessage("Your password must contain at least one number.");
            //Şifre ile ilgili kısıtylamlar ekle
        }
    }
}
