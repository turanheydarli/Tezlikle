namespace Application.Utilities.Security.JWT;

public class AccessToken:IAccessToken
{
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
    public string RefreshToken { get; set; }
}