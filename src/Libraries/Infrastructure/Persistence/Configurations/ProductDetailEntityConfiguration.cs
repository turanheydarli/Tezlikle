using Domain.Catalog;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Common;

namespace Infrastructure.Persistence.Configurations;

public class ProductDetailEntityConfiguration:BaseConfiguration<ProductDetail>
{
    public override void Configure(EntityTypeBuilder<ProductDetail> builder)
    {
        builder.Property(p => p.Title).HasMaxLength((int)MaxLengthSize.Title).IsRequired();
        builder.Property(p => p.Description).HasMaxLength((int)MaxLengthSize.Content).IsRequired();
        builder.Property(p => p.SeoDescription).HasMaxLength((int)MaxLengthSize.Description).IsRequired();
        builder.Property(p => p.SeoKeywords).HasMaxLength((int)MaxLengthSize.Title).IsRequired();
        builder.Property(p => p.SeoTitle).HasMaxLength((int)MaxLengthSize.Title).IsRequired();
        
        base.Configure(builder);
    }
}