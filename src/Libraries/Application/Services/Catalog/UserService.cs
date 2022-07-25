using Application.Models.Users;
using Application.Services.Catalog.Interfaces;
using Application.Services.Media.Interfaces;
using Application.ValidationRules.FluentValidation.Users;
using AutoMapper;
using Domain.Media;
using Domain.User;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Shared.Common;
using Shared.Common.Messages;
using Shared.Utilities.Application;
using Shared.Utilities.Aspects.Autofac.Validation;
using Shared.Utilities.Results;

namespace Application.Services.Catalog;

public class UserService:IUserService
{
    #region Fields
    private IRepository<User> _userRepository;
    private IMapper _mapper;
    private IPictureService _pictureService;
    #endregion

    #region Ctor

    public UserService(IRepository<User> userRepository, IMapper mapper, IPictureService pictureService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _pictureService = pictureService;
    }
    #endregion
    
    #region Methods
    [ValidationAspect(typeof(UserModelValidator))]
    public IDataResult<UserModel> CreateUser(UserModel userModel)
    {
        var ruleError = BusinessRules.Run(CheckIfUsernameExist(userModel.Username));

        if (ruleError != null) 
            return new ErrorDataResult<UserModel>(message:ruleError.Message);

        userModel.Avatar = _pictureService.SetDefaultPicture(MediaDefaults.DefaultAvatarImage);
        userModel.CoverImage = _pictureService.SetDefaultPicture(MediaDefaults.DefaultCoverImage);
        
        var user = _userRepository.Insert(_mapper.Map<User>(userModel));
        
        if (user.Id == 0)
        {
            return new ErrorDataResult<UserModel>(userModel, Messages.UserNotCreated);
        }
        
        return new SuccessDataResult<UserModel>(_mapper.Map<UserModel>(user));
    }

    public IDataResult<UserModel> UpdateUser(UserModel userModel)
    {
        var ruleError = BusinessRules.Run(CheckIfUsernameExist(userModel.Username));

        if (ruleError != null) 
            return new ErrorDataResult<UserModel>(message:ruleError.Message);

        var user = _userRepository.GetById(userModel.Id);

        if (user == null)
        {
            return new ErrorDataResult<UserModel>(Messages.UserNotFound);
        }

        if (userModel.AvatarFile != null)
        {
            //Save avatar image file
        }
        
        if (userModel.CoverImageType != null)
        {
            //Save cover image file
        }
        
        user.About = userModel.About;
        user.Email = userModel.Email;
        user.Slug = userModel.Slug;
        user.Username = userModel.Username;
        user.EmailStatus = userModel.EmailStatus;
        user.FirstName = userModel.FirstName;
        user.LastName = userModel.Slug;
        user.PhoneNumber = userModel.PhoneNumber;
        user.ShopName = userModel.ShopName;
        user.UserType = userModel.UserType;
        user.UpdatedTime = DateTime.UtcNow;

        var result = _userRepository.Update(user);
        
        if (result == -1)
        {
            return new ErrorDataResult<UserModel>(userModel, Messages.UserNotUpdated);
        }
        
        return new SuccessDataResult<UserModel>(_mapper.Map<UserModel>(user));
    }

    public IResult DeleteUserByUsername(string username)
    {
        User user = _userRepository.Get(u => u.Username == username);
        
        if (user == null)
        {
            return new ErrorDataResult<UserModel>(Messages.UserNotFound);
        }

        user.IsDeleted = true;

        var result = _userRepository.Update(user);

        if (result == -1)
            return new ErrorResult(Messages.UserNotDeleted);

        return new SuccessResult();
    }

    public IDataResult<UserModel> GetUserByUsername(string username)
    {
        var user = _userRepository.Get(u => u.Username == username);
        
        if (user == null)
        {
            return new ErrorDataResult<UserModel>(Messages.UserNotFound);
        }

        return new SuccessDataResult<UserModel>(_mapper.Map<UserModel>(user));
    }

    public IDataResult<UserModel> GetUserByRefreshToken(string refreshToken)
    {
        var user = _userRepository.Get(u => u.RefreshToken == refreshToken);
        
        if (user == null)
        {
            return new ErrorDataResult<UserModel>(Messages.UserNotFound);
        }

        return new SuccessDataResult<UserModel>(_mapper.Map<UserModel>(user));
    }

    public IDataResult<List<UserModel>> GetAllUsers()
    {
        var users = _userRepository.GetAll.ToList();

        return new SuccessDataResult<List<UserModel>>(_mapper.Map<List<UserModel>>(users));
    }

    public IDataResult<List<OperationClaim>> GetClaims(string username)
    {
        IQueryable<User> query = _userRepository.GetAllNoTracking
            .Where(u => u.Username == username)
            .Include(u => u.UserOperationClaims)
            .ThenInclude(u => u.OperationClaim);

        var user = query.FirstOrDefault();

        if (user == null)
        {
            return new ErrorDataResult<List<OperationClaim>>(Messages.UserNotFound);
        }
        
        List<OperationClaim> operationClaims = user.UserOperationClaims.Select(x => x.OperationClaim).ToList();

        return new SuccessDataResult<List<OperationClaim>>(operationClaims);
    }

    #endregion

    #region Utilities

    private IResult CheckIfUsernameExist(string username)
    {
        return _userRepository.Any(u => u.Username == username) 
            ? new ErrorResult(Messages.UserAlreadyExist) : new SuccessResult();
    }
    #endregion
}