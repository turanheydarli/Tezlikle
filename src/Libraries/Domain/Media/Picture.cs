using Domain.Common;

namespace Domain.Media;

public class Picture:BaseEntity
{
    public string ImageDefault { get; set; }
    public string ImageBig { get; set; }
    public string ImageSmall { get; set; }
    public string MimeType { get; set; }
    public PictureType PictureType { get; set; }
    public bool IsMain { get; set; }
}
