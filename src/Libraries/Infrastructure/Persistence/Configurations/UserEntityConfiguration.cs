using Domain.User;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Common;

namespace Infrastructure.Persistence.Configurations;

public class UserEntityConfiguration:BaseConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Email).HasMaxLength((int)MaxLengthSize.Email).IsRequired();
        builder.Property(u => u.Username).HasMaxLength((int)MaxLengthSize.Name).IsRequired();
        builder.Property(u => u.FirstName).HasMaxLength((int)MaxLengthSize.Name);
        builder.Property(u => u.LastName).HasMaxLength((int)MaxLengthSize.Name);
        builder.Property(u => u.ShopName).HasMaxLength((int)MaxLengthSize.Name);
        builder.Property(u => u.About).HasMaxLength((int)MaxLengthSize.Content);
        
        base.Configure(builder);
    }
}