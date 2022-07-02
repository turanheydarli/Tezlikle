using Application.Models.Users;
using Application.Models.Users;
using Shared.Utilities.Results;

namespace Application.Services.Authorization.Interfaces;

public interface IAuthService
{
    IResult Register(UserRegisterModel registerModel);
    UserLoginResult Login(UserLoginModel loginModel);
    UserLoginResult LoginWithRefreshToken(string refreshToken);
}