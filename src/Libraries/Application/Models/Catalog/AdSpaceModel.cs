using Application.Core.Mapping;
using AutoMapper;
using Domain.Catalog;

namespace Application.Models.Catalog;

public class AdSpaceModel:IMapFrom<AdSpace>
{
    public int Id { get; set; }
    public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedTime { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<AdSpace, AdSpaceModel>()
            .ForMember(b => b.Id, opt => opt.MapFrom(d => d.Id))
            .ForMember(b => b.CreatedTime, opt => opt.MapFrom(d => d.CreatedTime))
            .ForMember(b => b.UpdatedTime, opt => opt.MapFrom(d => d.UpdatedTime))
            .ForMember(b => b.Name, opt => opt.MapFrom(d => d.Name))
            .ForMember(b => b.Code, opt => opt.MapFrom(d => d.Code))
            .ReverseMap();
    }
    
}