using Application.Models.Users;
using FluentValidation;
using Shared.Common.Messages;

namespace Application.ValidationRules.FluentValidation.Users;

public class UserModelValidator : AbstractValidator<UserModel>
{
    public UserModelValidator()
    {
        RuleFor(x => x.Email).NotEmpty()
            .WithMessage(Messages.InvalidEmailAdress);
        
        RuleFor(x => x.Email).Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")
            .WithMessage(Messages.InvalidEmailAdress);

        RuleFor(x => x.Username).NotEmpty()
            .WithMessage(Messages.InvalidUsername);

        RuleFor(x => x.Username).Matches(@"^[a-zA-Z0-9](_(?!(\.|_))|\.(?!(_|\.))|[a-zA-Z0-9]){6,18}[a-zA-Z0-9]$")
            .WithMessage(Messages.InvalidUsername);

    }
}