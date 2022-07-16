using Application.Utilities.Security.JWT;

namespace Application.Services.Authentication;

/// <summary>
/// It is the return data of the login function.
/// </summary>
public class UserLoginResult
{
    public enum LoginStatus
    {
        UserNotFound,
        Ok,
        PhoneNumberRequired,
        ServiceError,
        WrongCredentials
    }

    /// <summary>
    /// Login query result.
    /// </summary>
    public LoginStatus Status { get; set; }

    /// <summary>
    /// Additional message
    /// </summary>
    public string Message { get; set; }
    
    /// <summary>
    /// Additional message
    /// </summary>
    public AccessToken AccessToken { get; set; }
    
}