using Domain.Common;

namespace Domain.Catalog;

public class Region:BaseEntity
{
    public string Name { get; set; }
    public decimal Longitude { get; set; }
    public decimal Latitude { get; set; }
}