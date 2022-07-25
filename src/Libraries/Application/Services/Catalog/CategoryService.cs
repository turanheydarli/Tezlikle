using Application.Models.Catalog;
using Application.Services.Catalog.Interfaces;
using Application.Services.Media.Interfaces;
using Application.ValidationRules.FluentValidation.Catalog;
using AutoMapper;
using Domain.Catalog;
using Domain.Media;
using Infrastructure.Persistence;
using Shared.Common;
using Shared.Common.Messages;
using Shared.Utilities.Application;
using Shared.Utilities.Aspects.Autofac.Authorization;
using Shared.Utilities.Aspects.Autofac.Caching;
using Shared.Utilities.Aspects.Autofac.Validation;
using Shared.Utilities.Results;

namespace Application.Services.Catalog;

public class CategoryService:ICategoryService
{
    #region Fields

    private readonly IMapper _mapper;
    private readonly IRepository<Category> _categoryRepository;
    private IPictureService _pictureService;

    #endregion

    #region Ctro

    public CategoryService(IRepository<Category> categoryRepository, IMapper mapper, IPictureService pictureService)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _pictureService = pictureService;
    }

    #endregion
    
    #region Methods

    [CacheAspect]
    public IDataResult<List<CategoryModel>> GetAllCategories()
    {
        var categories = _categoryRepository.GetAll.ToList();

        return new SuccessDataResult<List<CategoryModel>>(_mapper.Map<List<CategoryModel>>(categories));
    }
    
    [CacheAspect]
    public IDataResult<List<CategoryModel>> GetCategoryById(int id)
    {
        var category = _categoryRepository
            .Get(c => c.Id == id, c => c.Params,c=>c.Parent);

        if (category == null)
            return new ErrorDataResult<List<CategoryModel>>(Messages.CategoryNotFound);

        return new SuccessDataResult<List<CategoryModel>>(_mapper.Map<List<CategoryModel>>(category));
    }

    [CacheAspect]
    public IDataResult<CategoryModel> GetCategoryBySlug(string slug)
    {
        var category = _categoryRepository.Get(c => c.Slug == slug && c.Visibility, 
                                            c => c.Params,c=>c.Parent);

        if (category == null)
            return new ErrorDataResult<CategoryModel>(Messages.CategoryNotFound);

        return new SuccessDataResult<CategoryModel>(_mapper.Map<CategoryModel>(category));
    }

    [SecuredOperation("Category.Update,Admin")]
    [ValidationAspect(typeof(CategoryModelValidator))]
    [CacheRemoveAspect("ICategoryService.Get*")]
    public IDataResult<CategoryModel> UpdateCategory(CategoryModel categoryModel)
    {
        var category = _categoryRepository.Get(c => c.Id == categoryModel.Id);
        
        if (category == null)
            return new ErrorDataResult<CategoryModel>(Messages.CategoryNotFound);

        category.Description = categoryModel.Description;
        category.Keywords = categoryModel.Keywords;
        category.Slug = categoryModel.Keywords;
        category.Title = categoryModel.Title;
        category.Visibility = categoryModel.Visibility;
        category.CategoryOrder = categoryModel.CategoryOrder;
        category.FeaturedOrder = categoryModel.FeaturedOrder;
        category.HomepageOrder = categoryModel.HomepageOrder;
        category.IsFeatured = categoryModel.IsFeatured;
        category.ParentId = categoryModel.ParentId;
        category.ShowImageOnNavigation = categoryModel.ShowImageOnNavigation;
        
        category.UpdatedTime = DateTime.UtcNow;

        var updateResult = _categoryRepository.Update(category);

        if (updateResult == -1)
            return new ErrorDataResult<CategoryModel>(Messages.CategoryNotUpdated);

        return new SuccessDataResult<CategoryModel>(_mapper.Map<CategoryModel>(category));
    }
    
    [SecuredOperation("Category.Create,Admin")]
    [CacheRemoveAspect("ICategoryService.Get*")]
    public IDataResult<CategoryModel> CreateCategory(CategoryModel categoryModel)
    {
        // add rules to parameter
        var ruleResult = BusinessRules.Run();

        if (ruleResult != null)
            return new ErrorDataResult<CategoryModel>(ruleResult.Message);

        categoryModel.Picture = _pictureService.SetDefaultPicture(MediaDefaults.DefaultCategoryImage);

        if (categoryModel.PictureFile != null)
        {
            categoryModel.Picture = _pictureService.InsertPicture(categoryModel.PictureFile, MediaDefaults.CategoryPath, PictureType.Entity);
        }

        var insertResult = _categoryRepository.Insert(_mapper.Map<Category>(categoryModel));

        if (insertResult.Id == 0)
            return new ErrorDataResult<CategoryModel>(Messages.CategoryNotCreated);

        return new SuccessDataResult<CategoryModel>(_mapper.Map<CategoryModel>(insertResult));
    }

    [SecuredOperation("Category.Delete,Admin")]
    [CacheRemoveAspect("ICategoryService.Get*")]
    public IResult DeleteCategoryById(int id)
    {
        var category = _categoryRepository.Get(c => c.Id == id);
        
        if (category == null)
            return new ErrorDataResult<CategoryModel>(Messages.CategoryNotFound);

        category.Visibility = false;

        var updateResult = _categoryRepository.Update(category);

        if (updateResult == -1)
            return new ErrorResult(Messages.CategoryNotDeleted);

        return new SuccessResult(Messages.CategoryDeleted);
    }
    
    [CacheAspect]
    public IDataResult<List<CategoryModel>> GetAllCategoriesOrderedByName()
    {
        var categories = _categoryRepository.GetAll
            .Where(c => c.Visibility)
            .OrderBy(c => c.Name).ToList();

        return new SuccessDataResult<List<CategoryModel>>(_mapper.Map<List<CategoryModel>>(categories));
    }
    
    [CacheAspect]
    public IDataResult<List<CategoryModel>> GetAllCategoriesOrderedBySlug()
    {
        var categories = _categoryRepository
            .GetMany(c => c.Visibility)
            .OrderBy(c => c.Slug).ToList();

        return new SuccessDataResult<List<CategoryModel>>(_mapper.Map<List<CategoryModel>>(categories));
    }

    [CacheAspect]
    public IDataResult<List<CategoryModel>> GetAllFeaturedCategories()
    {
        var categories = _categoryRepository
            .GetMany(c => c.Visibility && c.IsFeatured)
            .OrderBy(c => c.FeaturedOrder).ToList();
        
        
        return new SuccessDataResult<List<CategoryModel>>(_mapper.Map<List<CategoryModel>>(categories));
    }

    [CacheAspect]
    public IDataResult<List<CategoryModel>> GetAllIndexCategories()
    {
        var categories = _categoryRepository
            .GetMany(c => c.Visibility && c.ShowProductsOnIndex)
            .OrderBy(c => c.HomepageOrder).ToList();
        
        
        return new SuccessDataResult<List<CategoryModel>>(_mapper.Map<List<CategoryModel>>(categories));
    }

    #endregion
}