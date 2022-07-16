using Application.Core.Mapping;
using AutoMapper;
using Domain.Catalog;
using Domain.Media;
using Microsoft.AspNetCore.Http;

namespace Application.Models.Catalog;

public class CategoryModel:IMapFrom<Category>
{
    public int Id { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime? UpdatedTime { get; set; }
    
    public string Slug { get; set; }
    public int? ParentId { get; set; }
    public Category Parent { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Keywords { get; set; }
    public int CategoryOrder { get; set; }
    public int HomepageOrder { get; set; }
    public bool IsFeatured { get; set; }
    public int FeaturedOrder { get; set; }
    public bool Visibility { get; set; }
    public int PictureId { get; set; }
    public Picture Picture { get; set; }
    public bool ShowImageOnNavigation { get; set; }
    public bool ShowProductsOnIndex { get; set; }
    public ICollection<Param> Params { get; set; }
    public string Name { get; set; }

    public IFormFile PictureFile { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Category, CategoryModel>()
            .ForMember(b => b.Id, opt => opt.MapFrom(d => d.Id))
            .ForMember(b => b.CreatedTime, opt => opt.MapFrom(d => d.CreatedTime))
            .ForMember(b => b.UpdatedTime, opt => opt.MapFrom(d => d.UpdatedTime))
            .ForMember(b => b.Name, opt => opt.MapFrom(d => d.Name))
            .ForMember(b => b.Slug, opt => opt.MapFrom(d => d.Slug))
            .ForMember(b => b.ParentId, opt => opt.MapFrom(d => d.ParentId))
            .ForMember(b => b.Parent, opt => opt.MapFrom(d => d.Parent))
            .ForMember(b => b.Title, opt => opt.MapFrom(d => d.Title))
            .ForMember(b => b.Params, opt => opt.MapFrom(d => d.Params))
            .ForMember(b => b.ShowImageOnNavigation, opt => opt.MapFrom(d => d.ShowImageOnNavigation))
            .ForMember(b => b.PictureId, opt => opt.MapFrom(d => d.PictureId))
            .ForMember(b => b.Picture, opt => opt.MapFrom(d => d.Picture))
            .ForMember(b => b.ShowImageOnNavigation, opt => opt.MapFrom(d => d.ShowImageOnNavigation))
            .ForMember(b => b.Visibility, opt => opt.MapFrom(d => d.Visibility))
            .ForMember(b => b.FeaturedOrder, opt => opt.MapFrom(d => d.FeaturedOrder))
            .ForMember(b => b.HomepageOrder, opt => opt.MapFrom(d => d.HomepageOrder))
            .ForMember(b => b.CategoryOrder, opt => opt.MapFrom(d => d.CategoryOrder))
            .ForMember(b => b.IsFeatured, opt => opt.MapFrom(d => d.IsFeatured))
            .ForMember(b => b.Keywords, opt => opt.MapFrom(d => d.Keywords))
            .ForMember(b => b.Description, opt => opt.MapFrom(d => d.Description))
            .ForMember(b => b.Title, opt => opt.MapFrom(d => d.Title))
            .ReverseMap();
    }
}