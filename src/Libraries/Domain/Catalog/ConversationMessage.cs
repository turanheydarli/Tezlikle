using Domain.Common;

namespace Domain.Catalog;

public class ConversationMessage:BaseEntity
{
    public int SenderId { get; set; }
    public User.User Sender { get; set; }
    public int ReceiverId { get; set; }
    public User.User Receiver { get; set; }
    public string Subject { get; set; }
    public int ConversationId { get; set; }
    public Conversation Conversation { get; set; }
    public string Message { get; set; }
    public bool IsRead { get; set; }
    public int DeletedUserId { get; set; }
    public User.User DeletedUser { get; set; }
}