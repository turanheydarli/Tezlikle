using Application.Models.Catalog;
using Application.Services.Catalog.Interfaces;
using Application.Services.Media.Interfaces;
using Application.ValidationRules.FluentValidation.Catalog;
using AutoMapper;
using Domain.Catalog;
using Domain.Media;
using Infrastructure.Persistence;
using Shared.Common.Messages;
using Shared.Utilities.Application;
using Shared.Utilities.Aspects.Autofac.Authorization;
using Shared.Utilities.Aspects.Autofac.Caching;
using Shared.Utilities.Aspects.Autofac.Validation;
using Shared.Utilities.Results;
using Shared.Utilities.Seo;

namespace Application.Services.Catalog;

public class ProductService:IProductService
{
    #region Fields

    private readonly IRepository<Product> _productRepository;
    private readonly IMapper _mapper;
    private readonly IPictureService _pictureService;
    
    #endregion

    #region Ctor

    public ProductService(IRepository<Product> productRepository, IMapper mapper, IPictureService pictureService)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _pictureService = pictureService;
    }

    #endregion
    
    #region Methods

    [SecuredOperation("Product.Add,User,Admin")]
    [CacheRemoveAspect("IProductService.Get*")]
    [ValidationAspect(typeof(ProductModelValidator))]
    public IDataResult<ProductModel> AddProduct(ProductModel productModel)
    {
        //add rules to parameter
        var ruleError = BusinessRules.Run();

        if (ruleError != null)
            return new ErrorDataResult<ProductModel>(ruleError.Message);

        productModel.Slug ??= SlugHelper.Create(productModel.ProductDetail.Title); 

        if (productModel.PictureFiles != null)
        {
            var pictures = productModel.PictureFiles
                .Select(picture => _pictureService.InsertPicture(picture, MediaDefaults.EntityPath, PictureType.Entity))
                .ToList();

            productModel.ProductDetail.Pictures = pictures;
        }

        var insertResult = _productRepository.Insert(_mapper.Map<Product>(productModel));

        if (insertResult.Id == 0)
            return new ErrorDataResult<ProductModel>(Messages.ProductNotAdded);

        return new SuccessDataResult<ProductModel>(_mapper.Map<ProductModel>(insertResult));
    }

    [SecuredOperation("Product.Update,User,Admin")]
    [ValidationAspect(typeof(ProductModelValidator))]
    [CacheRemoveAspect("IProductService.Get*")]
    public IDataResult<ProductModel> UpdateProduct(ProductModel productModel)
    {
        var product = _productRepository.GetById(productModel.Id);

        if (product == null)
            return new ErrorDataResult<ProductModel>(Messages.ProductNotFound);

        product.CategoryId = productModel.CategoryId;
        product.CurrencyId = productModel.CurrencyId;
        product.Price = productModel.Price;
        product.Sku = productModel.Sku;
        product.Slug = SlugHelper.Create(productModel.Slug);
        product.Status = productModel.Status;
        product.Visibility = productModel.Visibility;
        product.ContactNumber = productModel.ContactNumber;
        product.IsDeleted = productModel.IsDeleted;
        product.IsDraft = productModel.IsDraft;
        product.IsSold = productModel.IsSold;
        product.UpdatedTime =DateTime.UtcNow;

        var updateResult = _productRepository.Update(product);

        if (updateResult == -1)
            return new ErrorDataResult<ProductModel>(productModel, Messages.ProductNotUpdated);

        return new SuccessDataResult<ProductModel>(_mapper.Map<ProductModel>(product));
    }
    
    public IDataResult<List<ProductModel>> FilterProducts(string search, int priceMin, int priceMax, int categoryId = 0, int userId = 0)
    {
        var query = _productRepository.GetMany(null,
            p => p.Currency, p => p.Category, p => p.ProductDetail);

        if (search != null)
        {
            var searchArray = search.Split(' ');

            query = searchArray.Aggregate(query, (current, searchKey) => current.Where(p => p.ProductDetail.Title.Contains(searchKey)));

            query = query.OrderBy(p => p.IsPromoted);
            
        }

        if (priceMax != 0)
            query = query.Where(p => p.Price <= priceMax);
        
        if (priceMin != 0)
            query = query.Where(p => p.Price >= priceMin);

        if (categoryId != 0)
            query = query.Where(p => p.CategoryId == categoryId);
        
        if (userId != 0)
            query = query.Where(p => p.CategoryId == userId);

        var result = query.ToList();

        return new SuccessDataResult<List<ProductModel>>(_mapper.Map<List<ProductModel>>(result));
    }

    [CacheAspect]
    public IDataResult<List<ProductModel>> SearchProducts(string search)
    {
        var query = _productRepository.GetMany(null,
            p => p.Currency, p => p.Category, p => p.ProductDetail);

        if (search != null)
        {
            var searchArray = search.Split(' ');

            query = searchArray.Aggregate(query, (current, searchKey) => current.Where(p => p.ProductDetail.Title.Contains(searchKey)));

            query = query.OrderByDescending(p => p.Id).ThenBy(p => p.IsPromoted);
        }
        
        var result = query.ToList();

        return new SuccessDataResult<List<ProductModel>>(_mapper.Map<List<ProductModel>>(result));
    }

    [CacheAspect]
    public IDataResult<List<ProductModel>> GetAllProducts()
    {
        var products = _productRepository.GetMany(null, p => p.ProductDetail);

        return new SuccessDataResult<List<ProductModel>>(_mapper.Map<List<ProductModel>>(products.ToList()));
    }

    [CacheAspect]
    public IDataResult<List<ProductModel>> GetPromotedProducts()
    {
        var products = _productRepository.GetMany(p => p.IsPromoted, p => p.ProductDetail);
        
        return new SuccessDataResult<List<ProductModel>>(_mapper.Map<List<ProductModel>>(products.ToList()));
    }

    [CacheAspect]
    public IDataResult<List<ProductModel>> GetProductsByCategoryId(int categoryId)
    {
        var products = _productRepository.GetMany(p => p.CategoryId == categoryId, p => p.ProductDetail);

        return new SuccessDataResult<List<ProductModel>>(_mapper.Map<List<ProductModel>>(products.ToList()));
    }

    [SecuredOperation("Product.Delete,User,Admin")]
    [CacheRemoveAspect("IProductService.Get*")]
    public IResult DeleteProductById(int id)
    {
        var product = _productRepository.GetById(id);

        product.IsDeleted = true;

        var update = _productRepository.Update(product);

        if (update == -1)
            return new ErrorResult(Messages.ProductNotDeleted);

        return new SuccessResult(Messages.ProductDeleted);
    }

    #endregion
    
}