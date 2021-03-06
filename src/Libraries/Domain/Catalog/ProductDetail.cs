using Domain.Common;
using Domain.Media;

namespace Domain.Catalog;

public class ProductDetail:BaseEntity
{
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string SeoTitle { get; set; }
    public string SeoDescription { get; set; }
    public string SeoKeywords { get; set; }
    public IEnumerable<Picture> Pictures { get; set; }
    public int RegionId { get; set; }
    public Region Region { get; set; }
    public ICollection<Comment> Comments { get; set; }
}