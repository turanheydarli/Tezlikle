using Domain.Common;
using Domain.Media;

namespace Domain.Catalog;

public class Category:BaseEntity
{
    public string Slug { get; set; }
    public int? ParentId { get; set; }
    public Category Parent { get; set; }
    public string Title { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Keywords { get; set; }
    public int CategoryOrder { get; set; }
    public int HomepageOrder { get; set; }
    public bool ShowProductsOnIndex { get; set; }
    public bool IsFeatured { get; set; }
    public int FeaturedOrder { get; set; }
    public bool Visibility { get; set; }
    public int PictureId { get; set; }
    public Picture Picture { get; set; }
    public bool ShowImageOnNavigation { get; set; }
    public ICollection<Param> Params { get; set; }
}