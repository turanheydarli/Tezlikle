using Application.Models.Catalog;
using Shared.Utilities.Results;

namespace Application.Services.Catalog.Interfaces;

public interface IProductService
{
    /// <summary>
    /// Inserts new product
    /// </summary>
    /// <param name="productModel"></param>
    /// <returns></returns>
    IDataResult<ProductModel> AddProduct(ProductModel productModel);
    
    /// <summary>
    /// Updates product
    /// </summary>
    /// <param name="productModel"></param>
    /// <returns></returns>
    IDataResult<ProductModel> UpdateProduct(ProductModel productModel);

    /// <summary>
    /// Filter products
    /// </summary>
    /// <param name="search"></param>
    /// <param name="priceMax"></param>
    /// <param name="categoryId"></param>
    /// <param name="userId"></param>
    /// <param name="priceMin"></param>
    /// <returns></returns>
    IDataResult<List<ProductModel>> FilterProducts(string search, int priceMin, int priceMax ,int categoryId = 0, int userId = 0);
    
    /// <summary>
    /// Search products
    /// </summary>
    /// <param name="search"></param>
    /// <returns></returns>
    IDataResult<List<ProductModel>> SearchProducts(string search);
    
    /// <summary>
    /// Gets all products
    /// </summary>
    /// <returns></returns>
    IDataResult<List<ProductModel>> GetAllProducts();
    
    /// <summary>
    /// Gets promoted products
    /// </summary>
    /// <returns></returns>
    IDataResult<List<ProductModel>> GetPromotedProducts();
    
    /// <summary>
    /// Gets products by category id 
    /// </summary>
    /// <returns></returns>
    IDataResult<List<ProductModel>> GetProductsByCategoryId(int categoryId);

    /// <summary>
    /// Deletes product 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    IResult DeleteProductById(int id);

}