using E_commers.Application.DTOS.Identity;
using FluentValidation;

namespace E_commers.Application.Validations.Authentication
{
    public class LoginUserValidator : AbstractValidator<LoginUser> 
    {
        public LoginUserValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email Is Required")
                .EmailAddress().WithMessage("Invalid Email Formate");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("password is required");
            
        }
    }
}
