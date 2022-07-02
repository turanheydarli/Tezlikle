using Application.Models.Users;
using FluentValidation;
using Shared.Common.Messages;

namespace Application.ValidationRules.FluentValidation.Users;

public class UserRegisterModelValidator: AbstractValidator<UserRegisterModel>
{
    public UserRegisterModelValidator()
    {
        RuleFor(u => u.Agreement)
            .NotEmpty().WithMessage(Messages.RegisterAgreemenError);
        RuleFor(u => u.Email).EmailAddress()
            .NotEmpty().WithMessage(Messages.InvalidEmailAdress);
        RuleFor(u => u.Username).Matches(@"^[a-zA-Z0-9](_(?!(\.|_))|\.(?!(_|\.))|[a-zA-Z0-9]){6,18}[a-zA-Z0-9]$")
            .NotEmpty().WithMessage(Messages.InvalidUsername);
        RuleFor(u => u.FirstName)
            .NotEmpty().WithMessage(Messages.InvalidName);
        RuleFor(u => u.PasswordConfirm)
            .NotEmpty().Equal(x => x.Password)
            .WithMessage(Messages.RegisterAgreemenError);
    }
}