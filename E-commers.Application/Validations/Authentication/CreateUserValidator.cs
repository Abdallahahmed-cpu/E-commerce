using E_commers.Application.DTOS.Identity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commers.Application.Validations.Authentication
{
    public class CreateUserValidator : AbstractValidator<CreateUser>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Full Name Is Required");


            RuleFor(x => x.Email).NotEmpty().WithMessage("Email Is Required")
                .EmailAddress().WithMessage("Invalid Email Formate");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password Is Required")
                .MinimumLength(8).WithMessage("password must be 8 characters long")
                .Matches(@"[A-Z]").WithMessage("password must have at least one uppercase letter")
                .Matches(@"[a-z]").WithMessage("password must have at least one lowercase letter")
                .Matches(@"\d").WithMessage("password must have at least one number")
                .Matches(@"[^\w]").WithMessage("password must have at least one special caracter");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage("password do not match");


        }
    }
}
