using System.Security.Claims;
using Application.Models.Users;
using Application.Services.Authorization.Interfaces;
using Application.Services.Catalog.Interfaces;
using Application.Utilities.Security.Hashing;
using Application.Utilities.Security.JWT;
using Application.ValidationRules.FluentValidation.Users;
using AutoMapper;
using Domain.User;
using Microsoft.AspNetCore.Http;
using Shared.Common.Messages;
using Shared.Extensions;
using Shared.Utilities.Application;
using Shared.Utilities.Aspects.Autofac.Validation;
using Shared.Utilities.Results;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Application.Services.Authorization;

public class AuthService:IAuthService
{
   #region Fields

   private IHttpContextAccessor _httpContextAccessor;
   private IUserService _userService;
   private IMapper _mapper;
   private ITokenHelper _tokenHelper;
   #endregion

   #region Ctor

   public AuthService(IUserService userService, IMapper mapper, ITokenHelper tokenHelper, IHttpContextAccessor httpContextAccessor)
   {
      _userService = userService;
      _mapper = mapper;
      _tokenHelper = tokenHelper;
      _httpContextAccessor = httpContextAccessor;
   }
   #endregion
   
   #region Methods

   [ValidationAspect(typeof(UserRegisterModelValidator))]
   public IResult Register(UserRegisterModel registerModel)
   {
      byte[] passwordHash, passwordSalt;
      
      HashingHelper.CreatePasswordHash(registerModel.Password, out passwordHash,out passwordSalt);
      
      var user = _userService.CreateUser(new UserModel
      {
         Username = registerModel.Username,
         PasswordHash = passwordHash,
         PasswordSalt = passwordSalt,
         Email = registerModel.Email,
         FirstName = registerModel.FirstName,
         LastName = registerModel.LastName,
      });

      if (!user.Success)
      {
         return new ErrorResult(user.Message);
      }

      return new SuccessResult(Messages.UserRegisterSuccessed);
   }

   public UserLoginResult Login(UserLoginModel loginModel)
   {
      IDataResult<UserModel> user = _userService.GetUserByUsername(loginModel.Username);

      if (!user.Success || user.Data == null)
      {
         return new UserLoginResult
         {
            Message = user.Message,
            Status = UserLoginResult.LoginStatus.UserNotFound
         };
      }

      var result = BusinessRules.Run
      (
         CheckUserPassword(user.Data, loginModel.Password)
      );

      if (result != null)
      {
         return new UserLoginResult
         {
            Message = result.Message,
            Status = UserLoginResult.LoginStatus.WrongCredentials
         };
      }

      List<Claim> claims = new List<Claim>();

      string[] roles = _userService.GetClaims(user.Data.Username).Data.Select(c => c.Name).ToArray();

      claims.AddNameIdentifier(user.Data.Username);
      claims.AddName(user.Data.Username);
      claims.AddRoles(roles);

           
      var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
      var principal = new ClaimsPrincipal(identity);

      var accessToken = CreateAccessToken(user.Data.Username);

      user.Data.RefreshToken = accessToken.RefreshToken;

      _userService.UpdateUser(user.Data);

      _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

      return new UserLoginResult
      {
         Message = Messages.UserLoginSuccessful,
         Status = UserLoginResult.LoginStatus.Ok,
         AccessToken = accessToken
      };

   }

   public UserLoginResult LoginWithRefreshToken(string refreshToken)
   {
      IDataResult<UserModel> user = _userService.GetUserByRefreshToken(refreshToken);

      if (!user.Success)
      {
         return new UserLoginResult
         {
            Message = user.Message,
            Status = UserLoginResult.LoginStatus.UserNotFound
         };
      }

      List<Claim> claims = new List<Claim>();

      string[] roles = _userService.GetClaims(user.Data.Username).Data.Select(c => c.Name).ToArray();

      claims.AddNameIdentifier(user.Data.Username);
      claims.AddName(user.Data.Username);
      claims.AddRoles(roles);

           
      var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
      var principal = new ClaimsPrincipal(identity);

      var accessToken = CreateAccessToken(user.Data.Username);

      user.Data.RefreshToken = accessToken.RefreshToken;

      _userService.UpdateUser(user.Data);

      _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

      return new UserLoginResult
      {
         Message = Messages.UserLoginSuccessful,
         Status = UserLoginResult.LoginStatus.Ok,
         AccessToken = accessToken
      };
   }

   #endregion

   #region Utilities
   private IResult CheckUserPassword(UserModel userModel, string password)
   {
      var result = HashingHelper.VaidatePasswordHash(password, userModel.PasswordHash, userModel.PasswordSalt);
      if (!result)
      {
         return new ErrorResult(Messages.PasswordIsWrong);
      }
      return new SuccessResult();
   }
   
   private AccessToken CreateAccessToken(string username)
   {
      var claims = _userService.GetClaims(username);
      var user = _userService.GetUserByUsername(username);
      
      var accessToken = _tokenHelper.CreateToken(_mapper.Map<User>(user.Data), claims.Data);

      return accessToken;
   }
   #endregion
}