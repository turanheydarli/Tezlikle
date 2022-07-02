using Domain.Catalog;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Common;

namespace Infrastructure.Persistence.Configurations;

public class GeneralSettingEntityConfiguration:BaseConfiguration<GeneralSetting>
{
    public override void Configure(EntityTypeBuilder<GeneralSetting> builder)
    {
        builder.Property(g => g.AppName).HasMaxLength((int)MaxLengthSize.Name).IsRequired();

        builder.HasData(new GeneralSetting
        {
            Id = 1,
            Favicon = "",
            Logo = "",
            Version = "",
            AppName = "Tezlikle",
            CreatedTime = DateTime.UtcNow,
            EmailVerification = false,
            FeaturedCategories = true,
            SiteColor = "#FFFF",
        });
        
        base.Configure(builder);
    }
}