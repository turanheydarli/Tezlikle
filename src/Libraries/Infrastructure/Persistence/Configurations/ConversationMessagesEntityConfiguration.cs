using Domain.Catalog;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Common;

namespace Infrastructure.Persistence.Configurations;

public class ConversationMessagesEntityConfiguration:BaseConfiguration<ConversationMessage>
{
    public override void Configure(EntityTypeBuilder<ConversationMessage> builder)
    {
        builder.Property(c => c.Message).HasMaxLength((int)MaxLengthSize.Description).IsRequired();
        
        base.Configure(builder);
    }
}