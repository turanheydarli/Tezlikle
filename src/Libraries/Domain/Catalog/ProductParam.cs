using Domain.Common;

namespace Domain.Catalog;

public class ProductParam:BaseEntity
{
    public int ParamId { get; set; }
    public Param Param { get; set; }
    public string Value { get; set; }
}