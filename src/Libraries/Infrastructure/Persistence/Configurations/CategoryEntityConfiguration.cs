using Domain.Catalog;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Common;

namespace Infrastructure.Persistence.Configurations;

public class CategoryEntityConfiguration:BaseConfiguration<Category>
{
    public override void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(c => c.Title).HasMaxLength((int)MaxLengthSize.Title).IsRequired();
        builder.Property(c => c.Description).HasMaxLength((int)MaxLengthSize.Description).IsRequired();
        
        base.Configure(builder);
    }
}