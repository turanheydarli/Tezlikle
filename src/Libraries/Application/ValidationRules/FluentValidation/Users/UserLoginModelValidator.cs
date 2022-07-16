using Application.Models.Users;
using FluentValidation;
using Shared.Common.Messages;

namespace Application.ValidationRules.FluentValidation.Users;

public class UserLoginModelValidator:AbstractValidator<UserLoginModel>
{
    public UserLoginModelValidator()
    {
        RuleFor(u => u.Username).Matches(@"^[a-zA-Z0-9](_(?!(\.|_))|\.(?!(_|\.))|[a-zA-Z0-9]){6,18}[a-zA-Z0-9]$")
            .NotEmpty().WithMessage(Messages.InvalidUsername);
    }
}