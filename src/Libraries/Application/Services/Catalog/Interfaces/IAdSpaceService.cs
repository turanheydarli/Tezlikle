using Application.Models.Catalog;
using Shared.Utilities.Results;

namespace Application.Services.Catalog.Interfaces;

public interface IAdSpaceService
{
    IDataResult<List<AdSpaceModel>> GetAllAds();
    IDataResult<AdSpaceModel> GetAdsByName(string name);
    IDataResult<AdSpaceModel> UpdateAds(AdSpaceModel adSpaceModel);
}