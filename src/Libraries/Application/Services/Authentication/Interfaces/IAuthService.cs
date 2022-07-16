using Application.Models.Users;
using Shared.Utilities.Results;

namespace Application.Services.Authentication.Interfaces;

public interface IAuthService
{
    IResult Register(UserRegisterModel registerModel);
    UserLoginResult Login(UserLoginModel loginModel);
    IResult Logout();
    UserLoginResult LoginWithRefreshToken(string refreshToken);
}