using Domain.Common;

namespace Domain.Catalog;

public class Comment:BaseEntity
{
    public int ParentId { get; set; }
    public Comment Parent { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int UserId { get; set; }
    public User.User User { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Content { get; set; }
    public string IpAdress { get; set; }
    public bool Status { get; set; }
    
}