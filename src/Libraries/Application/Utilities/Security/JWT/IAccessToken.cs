namespace Application.Utilities.Security.JWT;

public interface IAccessToken
{
    DateTime Expiration { get; set; }
    string Token { get; set; }
    public string RefreshToken { get; set; }
}