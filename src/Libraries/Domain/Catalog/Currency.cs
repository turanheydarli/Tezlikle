using Domain.Common;

namespace Domain.Catalog;

public class Currency:BaseEntity
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }
    public string CurrencyFormat { get; set; }
    public bool Status { get; set; }
}