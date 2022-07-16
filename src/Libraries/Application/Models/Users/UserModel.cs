using Application.Core.Mapping;
using AutoMapper;
using Domain.Media;
using Domain.User;
using Microsoft.AspNetCore.Http;

namespace Application.Models.Users;

public class UserModel:IMapFrom<User>
{
    public int Id { get; set; }
    public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedTime { get; set; }
    public string Username { get; set; }
    public string Slug { get; set; }
    public string Email { get; set; }
    public string About { get; set; }
    public bool EmailStatus { get; set; }  
    public bool IsDeleted { get; set; }

    public string UserType { get; set; }
    public string CoverImageType { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ShopName { get; set; }
    public string PhoneNumber { get; set; }
    public string RefreshToken { get; set; }
    public DateTime LastSeen { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public List<UserOperationClaim> UserOperationClaims { get; set; }
    public int AvatarId { get; set; }
    public Picture Avatar { get; set; }
    public int CoverImageId { get; set; }
    public Picture CoverImage { get; set; }
    public IFormFile AvatarFile { get; set; }
    public IFormFile CoverImageFile { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<User, UserModel>()
            .ForMember(b => b.Id, opt => opt.MapFrom(d => d.Id))
            .ForMember(b => b.CreatedTime, opt => opt.MapFrom(d => d.CreatedTime))
            .ForMember(b => b.UpdatedTime, opt => opt.MapFrom(d => d.UpdatedTime))
            .ForMember(b => b.About, opt => opt.MapFrom(d => d.About))
            .ForMember(b => b.RefreshToken, opt => opt.MapFrom(d => d.RefreshToken))
            .ForMember(b => b.IsDeleted, opt => opt.MapFrom(d => d.IsDeleted))
            .ForMember(b => b.AvatarId, opt => opt.MapFrom(d => d.AvatarId))
            .ForMember(b => b.Avatar, opt => opt.MapFrom(d => d.Avatar))
            .ForMember(b => b.CoverImageId, opt => opt.MapFrom(d => d.CoverImageId))
            .ForMember(b => b.CoverImage, opt => opt.MapFrom(d => d.CoverImage))
            .ForMember(b => b.FirstName, opt => opt.MapFrom(d => d.FirstName))
            .ForMember(b => b.LastName, opt => opt.MapFrom(d => d.LastName))
            .ForMember(b => b.LastSeen, opt => opt.MapFrom(d => d.LastSeen))
            .ForMember(b => b.PasswordHash, opt => opt.MapFrom(d => d.PasswordHash))
            .ForMember(b => b.PasswordSalt, opt => opt.MapFrom(d => d.PasswordSalt))
            .ForMember(b => b.Username, opt => opt.MapFrom(d => d.Username))
            .ForMember(b => b.Email, opt => opt.MapFrom(d => d.Email))
            .ForMember(b => b.Slug, opt => opt.MapFrom(d => d.Slug))
            .ForMember(b => b.EmailStatus, opt => opt.MapFrom(d => d.EmailStatus))
            .ForMember(b => b.PhoneNumber, opt => opt.MapFrom(d => d.PhoneNumber))
            .ForMember(b => b.ShopName, opt => opt.MapFrom(d => d.ShopName))
            .ForMember(b => b.UserType, opt => opt.MapFrom(d => d.UserType))
            .ForMember(b => b.CoverImageType, opt => opt.MapFrom(d => d.CoverImageType))
            .ForMember(b => b.UserOperationClaims, opt => opt.MapFrom(d => d.UserOperationClaims))
            .ReverseMap();
    }

}