using Domain.Common;

namespace Domain.Languages;

public class Translate:BaseEntity
{
    public int LanguageId { get; set; }
    public Language Language { get; set; }
    public string Code { get; set; }
    public string Value { get; set; }
}