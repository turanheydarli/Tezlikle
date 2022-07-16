using Application.Models.Catalog;
using Application.Services.Catalog.Interfaces;
using Application.ValidationRules.FluentValidation.Catalog;
using AutoMapper;
using Domain.Catalog;
using Infrastructure.Persistence;
using Shared.Common.Messages;
using Shared.Utilities.Aspects.Autofac.Authorization;
using Shared.Utilities.Aspects.Autofac.Caching;
using Shared.Utilities.Aspects.Autofac.Validation;
using Shared.Utilities.Results;

namespace Application.Services.Catalog;

public class AdSpaceService:IAdSpaceService
{
   #region Fields

   private readonly IMapper _mapper;
   private readonly IRepository<AdSpace> _adSpaceRepository;

   #endregion

   #region Ctor

   public AdSpaceService(IMapper mapper, IRepository<AdSpace> adSpaceRepository)
   {
      _mapper = mapper;
      _adSpaceRepository = adSpaceRepository;
   }

   #endregion
   
   #region Methods

   [CacheAspect]
   public IDataResult<List<AdSpaceModel>> GetAllAds()
   {
      var ads = _adSpaceRepository.GetAll.ToList();

      return new SuccessDataResult<List<AdSpaceModel>>(_mapper.Map<List<AdSpaceModel>>(ads));
   }

   [CacheAspect]
   public IDataResult<AdSpaceModel> GetAdsByName(string name)
   {
      var ads = _adSpaceRepository.Get(a => a.Name == name);

      if (ads == null)
         return new ErrorDataResult<AdSpaceModel>(Messages.AdNotFound);

      return new SuccessDataResult<AdSpaceModel>(_mapper.Map<AdSpaceModel>(ads));
   }

   [SecuredOperation("AdSpace.Update,Admin")]
   [ValidationAspect(typeof(AdSpaceModelValidator))]
   [CacheRemoveAspect(pattern:"IAdSpaceService.Get*")]
   public IDataResult<AdSpaceModel> UpdateAds(AdSpaceModel adSpaceModel)
   {
      var ads = _adSpaceRepository.Get(a => a.Name == adSpaceModel.Name);

      if (ads == null)
         return new ErrorDataResult<AdSpaceModel>(message: Messages.AdNotFound);
      
      ads.Code = adSpaceModel.Code;
      ads.UpdatedTime = DateTime.UtcNow;

      var updateResult = _adSpaceRepository.Update(ads);

      if (updateResult == -1)
         return new ErrorDataResult<AdSpaceModel>(message: Messages.AdNotUpdated);

      return new SuccessDataResult<AdSpaceModel>(_mapper.Map<AdSpaceModel>(ads));
   }

   #endregion
}