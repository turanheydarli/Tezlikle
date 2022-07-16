using Application.Models.Catalog;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Shared.Common;
using Shared.Common.Messages;
using Shared.Extensions;

namespace Application.ValidationRules.FluentValidation.Catalog;

public class CategoryModelValidator:AbstractValidator<CategoryModel>
{
    public CategoryModelValidator()
    {
        RuleFor(c => c.PictureFile).Must(IsSupportedImage).When(c => c.PictureFile != null)
            .WithMessage(Messages.UnsupportedMediaFile);
        
        RuleFor(c => c.Keywords)
            .MaximumLength((int) MaxLengthSize.Keyword).WithMessage(Messages.KeywordLengthError);
        
        RuleFor(c => c.Name)
            .NotNull().MaximumLength((int) MaxLengthSize.Name).WithMessage(Messages.InvalidCategoryName);
        
        RuleFor(c => c.Slug).Matches(@"[a-zA-Z0-9,.;:_'\\s-]*").MaximumLength((int) MaxLengthSize.Slug)
            .WithMessage(Messages.InvalidSlugError);
        
        RuleFor(c => c.Title).MaximumLength((int) MaxLengthSize.Title)
            .WithMessage(Messages.InvalidCategoryDescriptionError);
        
    }

    private bool IsSupportedImage(IFormFile file) => file.IsSupportedImage();
}