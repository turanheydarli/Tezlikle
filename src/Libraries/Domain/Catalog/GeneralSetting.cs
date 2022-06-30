using Domain.Common;

namespace Domain.Catalog;

public class GeneralSetting:BaseEntity
{
    public string AppName { get; set; }
    public int MenuLimit { get; set; }
    public string RecaptchaSiteKey { get; set; }
    public string RecaptchaSecretKey { get; set; }
    public string RecaptchaLang { get; set; }
    public string CustomCssCodes { get; set; }
    public string CustomJsCodes { get; set; }
    public string MailProtocol { get; set; }
    public string MailHost { get; set; }
    public string MailPort { get; set; }
    public string MailUsername { get; set; }
    public string MailPassword { get; set; }
    public string MailTitle { get; set; }
    public bool EmailVerification { get; set; }
    public string FacebookAppId { get; set; }
    public string FacebookAppSecret { get; set; }
    public string GoogleClientId { get; set; }
    public string GoogleClientSecret { get; set; }
    public string GoogleAnalytics { get; set; }
    public string SiteColor { get; set; }
    public string Logo { get; set; }
    public string LogoEmail { get; set; }
    public string Favicon { get; set; }
    public string WatermarkImageLarge { get; set; }
    public string WatermarkImageMid { get; set; }
    public string WatermarkImageSmall { get; set; }
    public string WatermarkVrtAlignment { get; set; } //middle
    public string WatermarkHorAlignment { get; set; } //center
    public string Version { get; set; }
    public bool WatermarkProductImages { get; set; }
    public bool WatermarkThumbImages { get; set; }
    public bool PromotedProducts { get; set; }
    public bool FeaturedCategories { get; set; }
    public bool IndexPromotedProductsCount { get; set; }
    public bool IndexLatestProductsCount { get; set; }


}