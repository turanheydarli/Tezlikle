using Application.Models.Catalog;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Shared.Common;
using Shared.Common.Messages;
using Shared.Extensions;

namespace Application.ValidationRules.FluentValidation.Catalog;

public class ProductModelValidator:AbstractValidator<ProductModel>
{
    public ProductModelValidator()
    {
        RuleFor(p => p.Slug).Matches(@"[a-zA-Z0-9,.;:_'\\s-]*")
            .MaximumLength((int) MaxLengthSize.Slug)
            .WithMessage(Messages.InvalidSlugError);
        
        RuleFor(p => p.ContactNumber)
            .MaximumLength((int)MaxLengthSize.Phone).NotNull()
            .WithMessage(Messages.InvalidContactNumber);
        
        RuleFor(p => p.PictureFiles)
            .Must(IsSupportedImage).When(p => p.PictureFiles != null)
            .WithMessage(Messages.UnsupportedMediaFile);
        
        RuleFor(p => p.ProductDetail.Description)
            .MaximumLength((int) MaxLengthSize.Description)
            .WithMessage(Messages.InvalidProductDescriptionError);
        
        RuleFor(p => p.ProductDetail.Title)
            .MaximumLength((int) MaxLengthSize.Title)
            .WithMessage(Messages.InvalidProductTitleError);
        
        RuleFor(p => p.ProductDetail.SeoDescription)
            .MaximumLength((int) MaxLengthSize.Description)
            .WithMessage(Messages.InvalidProductDescriptionError);
        
        RuleFor(p => p.ProductDetail.SeoTitle)
            .MaximumLength((int) MaxLengthSize.Title)
            .WithMessage(Messages.InvalidProductTitleError);
        
        RuleFor(p => p.ProductDetail.SeoKeywords)
            .MaximumLength((int) MaxLengthSize.Keyword)
            .WithMessage(Messages.InvalidProductKeywordError);
        
    }
    
    private static bool IsSupportedImage(List<IFormFile> files)
    {
        return files.Select(file => file.IsSupportedImage()).FirstOrDefault(x => x);
    }
}