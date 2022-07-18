using Application.Core.Mapping;
using AutoMapper;
using Domain.Catalog;
using Microsoft.AspNetCore.Http;

namespace Application.Models.Catalog;

public class ProductModel:IMapFrom<Product>
{
    public int Id { get; set; }
    public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedTime { get; set; }
    public string Slug { get; set; }
    public string Sku { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public decimal Price { get; set; }
    public int CurrencyId { get; set; }
    public Currency Currency { get; set; }
    public bool Status { get; set; }
    public bool IsPromoted { get; set; }
    public DateTime PromotedStart { get; set; }
    public DateTime PromotedEnd { get; set; }
    public string Visibility { get; set; }
    public int Raiting { get; set; }
    public int PageViews { get; set; }
    public bool IsSold { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsDraft { get; set; }
    public string ContactNumber { get; set; }
    public int ProductDetailId { get; set; }
    public ProductDetail ProductDetail { get; set; }

    public List<IFormFile> PictureFiles { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Product, ProductModel>()
            .ForMember(b => b.Id, opt => opt.MapFrom(d => d.Id))
            .ForMember(b => b.CreatedTime, opt => opt.MapFrom(d => d.CreatedTime))
            .ForMember(b => b.UpdatedTime, opt => opt.MapFrom(d => d.UpdatedTime))
            .ForMember(b => b.Sku, opt => opt.MapFrom(d => d.Sku))
            .ForMember(b => b.CategoryId, opt => opt.MapFrom(d => d.CategoryId))
            .ForMember(b => b.IsDeleted, opt => opt.MapFrom(d => d.IsDeleted))
            .ForMember(b => b.Category, opt => opt.MapFrom(d => d.Category))
            .ForMember(b => b.Price, opt => opt.MapFrom(d => d.Price))
            .ForMember(b => b.CurrencyId, opt => opt.MapFrom(d => d.CurrencyId))
            .ForMember(b => b.Currency, opt => opt.MapFrom(d => d.Currency))
            .ForMember(b => b.Status, opt => opt.MapFrom(d => d.Status))
            .ForMember(b => b.IsPromoted, opt => opt.MapFrom(d => d.IsPromoted))
            .ForMember(b => b.PromotedStart, opt => opt.MapFrom(d => d.PromotedStart))
            .ForMember(b => b.Visibility, opt => opt.MapFrom(d => d.Visibility))
            .ForMember(b => b.PromotedEnd, opt => opt.MapFrom(d => d.PromotedEnd))
            .ForMember(b => b.Raiting, opt => opt.MapFrom(d => d.Raiting))
            .ForMember(b => b.PageViews, opt => opt.MapFrom(d => d.PageViews))
            .ForMember(b => b.Slug, opt => opt.MapFrom(d => d.Slug))
            .ForMember(b => b.IsSold, opt => opt.MapFrom(d => d.IsSold))
            .ForMember(b => b.IsDraft, opt => opt.MapFrom(d => d.IsDraft))
            .ForMember(b => b.ContactNumber, opt => opt.MapFrom(d => d.ContactNumber))
            .ForMember(b => b.ProductDetail, opt => opt.MapFrom(d => d.ProductDetail))
            .ForMember(b => b.ProductDetailId, opt => opt.MapFrom(d => d.ProductDetailId))
            .ReverseMap();
    }
}