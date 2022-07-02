using Domain.Catalog;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Common;

namespace Infrastructure.Persistence.Configurations;

public class CurrencyEntityConfiguration:BaseConfiguration<Currency>
{
    public override void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.Property(c => c.Code).HasMaxLength((int)MaxLengthSize.Currency).IsRequired();
        builder.Property(c => c.Name).HasMaxLength((int)MaxLengthSize.Name).IsRequired();
        builder.Property(c => c.Symbol).HasMaxLength(1).IsRequired();
        
        base.Configure(builder);
    }
}