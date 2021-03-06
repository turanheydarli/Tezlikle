using Domain.Common;
using Domain.Media;

namespace Domain.User;

public class User:BaseEntity
{
    public string Username { get; set; }
    public string Slug { get; set; }
    public string Email { get; set; }
    public string About { get; set; }
    public bool EmailStatus { get; set; }
    public bool IsDeleted { get; set; }
    public string RefreshToken { get; set; }
    public string UserType { get; set; }

    public int AvatarId { get; set; }
    public Picture Avatar { get; set; }
    public int CoverImageId { get; set; }
    public Picture CoverImage { get; set; }
    public string CoverImageType { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ShopName { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime LastSeen { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }

    public ICollection<UserOperationClaim> UserOperationClaims { get; set; }
}