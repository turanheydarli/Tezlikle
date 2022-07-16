using Domain.Media;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class PictureEntityConfiguration:BaseConfiguration<Picture>
{
    public override void Configure(EntityTypeBuilder<Picture> builder)
    {
        builder.HasData(new List<Picture>
        {
            new Picture
            {
                Id =1,
                ImageDefault = "avatar_default.png",
            },
            new Picture
            {
                Id =2,
                ImageDefault = "cover_default.png",
            },
            new Picture
            {
                Id =3,
                ImageDefault = "category_default.png",
            },
        });
            
        base.Configure(builder);
    }
}