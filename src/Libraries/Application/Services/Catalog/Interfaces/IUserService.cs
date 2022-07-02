using Application.Models.Users;
using Domain.User;
using Shared.Utilities.Results;

namespace Application.Services.Catalog.Interfaces;

public interface IUserService
{
    IDataResult<UserModel> CreateUser(UserModel userModel);
    IDataResult<UserModel> UpdateUser(UserModel userModel);
    IResult DeleteUserByUsername(string username);
    IDataResult<UserModel> GetUserByUsername(string username);
    IDataResult<UserModel> GetUserByRefreshToken(string refreshToken);
    IDataResult<List<UserModel>> GetAllUsers();
    IDataResult<List<OperationClaim>> GetClaims(string username);
}