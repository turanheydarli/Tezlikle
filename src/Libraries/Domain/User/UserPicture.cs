using Domain.Common;
using Domain.Media;

namespace Domain.User;

public class UserPicture:BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; }
    public int PictureId { get; set; }
    public Picture Picture { get; set; }
    public PictureType PictureType { get; set; }
}