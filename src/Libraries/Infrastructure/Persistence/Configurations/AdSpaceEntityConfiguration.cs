using Domain.Catalog;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Common;

namespace Infrastructure.Persistence.Configurations;

public class AdSpaceEntityConfiguration:BaseConfiguration<AdSpace>
{
    public override void Configure(EntityTypeBuilder<AdSpace> builder)
    {
        builder.Property(x => x.Code).IsRequired();
        builder.Property(x => x.Name).HasMaxLength((int)MaxLengthSize.Name).IsRequired();

        builder.HasData(new List<AdSpace>(){
            new AdSpace
            {
                Id = 1,
                Name = "index_1",
                Code = "",
                CreatedTime = DateTime.UtcNow,
                UpdatedTime = null
            },
            new AdSpace
            {
                Id = 2,
                Name = "index_2",
                Code = "",
                CreatedTime = DateTime.UtcNow,
                UpdatedTime = null
            },
            new AdSpace
            {
                Id = 3,
                Name = "products",
                Code = "",
                CreatedTime = DateTime.UtcNow,
                UpdatedTime = null
            },
            new AdSpace
            {
                Id = 4,
                Name = "products_sidebar",
                Code = "",
                CreatedTime = DateTime.UtcNow,
                UpdatedTime = null
            },
            new AdSpace
            {
                Id = 5,
                Name = "product",
                Code = "",
                CreatedTime = DateTime.UtcNow,
                UpdatedTime = null
            },
            new AdSpace
            {
                Id = 6,
                Name = "product_bottom",
                Code = "",
                CreatedTime = DateTime.UtcNow,
                UpdatedTime = null
            },
            new AdSpace
            {
                Id = 7,
                Name = "profile",
                Code = "",
                CreatedTime = DateTime.UtcNow,
                UpdatedTime = null
            },
            new AdSpace
            {
                Id = 8,
                Name = "profile_sidebar",
                Code = "",
                CreatedTime = DateTime.UtcNow,
                UpdatedTime = null
            },
        });
        
        base.Configure(builder);
    }
    
}