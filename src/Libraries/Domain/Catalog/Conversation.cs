using Domain.Common;

namespace Domain.Catalog;

public class Conversation:BaseEntity
{
    public int SenderId { get; set; }
    public User.User Sender { get; set; }
    public int ReceiverId { get; set; }
    public User.User Receiver { get; set; }
    public string Subject { get; set; }
}