using Domain.Catalog;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Common;

namespace Infrastructure.Persistence.Configurations;

public class ConversationEntityConfiguration:BaseConfiguration<Conversation>
{
    public override void Configure(EntityTypeBuilder<Conversation> builder)
    {
        builder.Property(c => c.Subject).HasMaxLength((int)MaxLengthSize.Title).IsRequired();
        
        base.Configure(builder);
    }
}