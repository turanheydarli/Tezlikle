using Domain.Catalog;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class AdSpaceEntityConfiguration:BaseConfiguration<AdSpace>
{
    public override void Configure(EntityTypeBuilder<AdSpace> builder)
    {
        builder.Property(x => x.Code).IsRequired();
        builder.Property(x => x.Name).HasMaxLength(10).IsRequired();

        base.Configure(builder);
    }
}