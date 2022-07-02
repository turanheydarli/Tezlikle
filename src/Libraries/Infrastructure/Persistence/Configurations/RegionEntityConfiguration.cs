using Domain.Catalog;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Common;

namespace Infrastructure.Persistence.Configurations;

public class RegionEntityConfiguration:BaseConfiguration<Region>
{
    public override void Configure(EntityTypeBuilder<Region> builder)
    {
        builder.Property(r => r.Name).HasMaxLength((int)MaxLengthSize.Name).IsRequired();
        
        base.Configure(builder);
    }
}