using Domain.Catalog;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Common;

namespace Infrastructure.Persistence.Configurations;

public class CommentEntityConfiguration:BaseConfiguration<Comment>
{
 public override void Configure(EntityTypeBuilder<Comment> builder)
 {
  builder.Property(c => c.Content).HasMaxLength((int)MaxLengthSize.Description).IsRequired();
  builder.Property(c => c.Name).HasMaxLength((int)MaxLengthSize.Name).IsRequired();
  builder.Property(c => c.Email).HasMaxLength((int)MaxLengthSize.Email).IsRequired();
  
  base.Configure(builder);
 }
}