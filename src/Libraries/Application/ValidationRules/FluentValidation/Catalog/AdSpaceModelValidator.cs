using Application.Models.Catalog;
using FluentValidation;
using Shared.Common;
using Shared.Common.Messages;

namespace Application.ValidationRules.FluentValidation.Catalog;

public class AdSpaceModelValidator:AbstractValidator<AdSpaceModel>
{
    public AdSpaceModelValidator()
    {
        RuleFor(a => a.Name).NotEmpty().MaximumLength((int)MaxLengthSize.Name)
                                                .WithMessage(Messages.InvalidAdName);
        
        RuleFor(a => a.Code).NotEmpty().WithMessage(Messages.InvalidAdCode);
    }
}