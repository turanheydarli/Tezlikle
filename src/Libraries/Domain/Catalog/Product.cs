using Domain.Common;
using Domain.Media;

namespace Domain.Catalog;

public class Product:BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedTime { get; set; }
    public string Slug { get; set; }
    public string Sku { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public decimal Price { get; set; }
    public int CurrencyId { get; set; }
    public Currency Currency { get; set; }
    public bool Status { get; set; }
    public bool IsPromoted { get; set; }
    public DateTime PromotedStart { get; set; }
    public DateTime PromotedEnd { get; set; }
    public string Visibility { get; set; }
    public int Raiting { get; set; }
    public int PageViews { get; set; }
    public bool IsSold { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsDraft { get; set; }
    public string ContactNumber { get; set; }
    public int ProductDetailId { get; set; }
    public ProductDetail ProductDetail { get; set; }
    
    
}