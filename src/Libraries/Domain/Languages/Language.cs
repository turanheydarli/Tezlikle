using Domain.Common;

namespace Domain.Languages;

public class Language:BaseEntity
{
    public string Name { get; set; }
    public string Code { get; set; }
}