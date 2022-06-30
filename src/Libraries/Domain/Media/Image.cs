using Domain.Catalog;
using Domain.Common;

namespace Domain.Media;

public class Image:BaseEntity
{
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public string ImageDefault { get; set; }
    public string ImageBig { get; set; }
    public string ImageSmall { get; set; }
    public bool IsMain { get; set; }
}
