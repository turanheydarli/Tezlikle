using Domain.Catalog;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Common;

namespace Infrastructure.Persistence.Configurations;

public class ParamEntityConfiguration:BaseConfiguration<Param>
{
    public override void Configure(EntityTypeBuilder<Param> builder)
    {
        builder.Property(p => p.Name).HasMaxLength((int)MaxLengthSize.Name).IsRequired();
        
        base.Configure(builder);
    }
}