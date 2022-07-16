using Application.Models.Catalog;
using Application.Services.Catalog.Interfaces;
using AutoMapper;
using Domain.Catalog;
using Infrastructure.Persistence;
using Shared.Utilities.Results;

namespace Application.Services.Catalog;

public class ProductService:IProductService
{
    #region Fields

    private IRepository<Product> _productRepository;
    private IMapper _mapper;
    
    #endregion

    #region Ctor

    public ProductService(IRepository<Product> productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    #endregion
    
    #region Methods

    public IDataResult<ProductModel> AddProduct(ProductModel productModel)
    {
        throw new NotImplementedException();
    }

    public IDataResult<ProductModel> UpdateProduct(ProductModel productModel)
    {
        throw new NotImplementedException();
    }

    public IDataResult<List<ProductModel>> FilterProducts(string search, int priceMin, int priceMax, int categoryId = 0, int userId = 0)
    {
        var query = _productRepository.GetMany(null,
            p => p.Currency, p => p.Category, p => p.ProductDetail);

        if (search != string.Empty)
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

    public IDataResult<List<ProductModel>> SearchProducts(string search)
    {
        var query = _productRepository.GetMany(null,
            p => p.Currency, p => p.Category, p => p.ProductDetail);

        if (search != string.Empty)
        {
            var searchArray = search.Split(' ');

            query = searchArray.Aggregate(query, (current, searchKey) => current.Where(p => p.ProductDetail.Title.Contains(searchKey)));

            query = query.OrderByDescending(p => p.Id).ThenBy(p => p.IsPromoted);
        }
        
        var result = query.ToList();

        return new SuccessDataResult<List<ProductModel>>(_mapper.Map<List<ProductModel>>(result));
    }

    public IDataResult<List<ProductModel>> GetAllProducts()
    {
        throw new NotImplementedException();
    }

    public IDataResult<List<ProductModel>> GetPromotedProducts()
    {
        throw new NotImplementedException();
    }

    public IDataResult<List<ProductModel>> GetProductsByCategoryId(int categoryId)
    {
        throw new NotImplementedException();
    }

    public IResult DeleteProductById(int id)
    {
        throw new NotImplementedException();
    }

    #endregion
    
}