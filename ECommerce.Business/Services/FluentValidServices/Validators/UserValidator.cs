using ECommerce.Entities.Concrete;
using ECommerce.Entities.Entities.UserEntities.Concrete.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Services.FluentValidServices.Validators
{
    public class UserValidator : AbstractValidator<UserRegisterDto>
    {
        public UserValidator()
        {
            // Email controls
            RuleFor(x => x.Email)
                .EmailAddress().NotNull()
                .NotEmpty()
                .WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("Invalid email format");
            // Emailconfirm has to be the same as Email itself
            RuleFor(x => x.EmailConfirm)
                .Equal(x => x.Email);


            // password rulings: more than 7 charachters, at least one upper, lower, speacial character and a number.
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Required")
                .MinimumLength(7).WithMessage("Your password length has to be more than 7.")
                .Matches("[A-Z]").WithMessage("Your password has to contain at least one uppercase character.")
                .Matches("[a-z]").WithMessage("Your password has to contain at least one lowercase character.")
                .Matches("[0-9]").WithMessage("Your password has to contain at least one number.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Your password has to contain at least one special character.");

            // Passwordconfirm has to be the same as Password itself
            RuleFor(x => x.PasswordConfirm)
                .Equal(x => x.Password);

            //Persons age hast to be more then 18 and the field ist required
            RuleFor(x => x.Birthdate)
                .NotEmpty().WithMessage("Required")
                .LessThan(DateTime.Now.AddYears(-18)).WithMessage("Your age must be greater than 18.");
            
        }
    }
}
