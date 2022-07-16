using Application.Models.Catalog;
using Shared.Utilities.Results;

namespace Application.Services.Catalog.Interfaces;

public interface ICategoryService
{
    IDataResult<List<CategoryModel>> GetAllCategories();
    IDataResult<List<CategoryModel>> GetCategoryById(int id);
    IDataResult<CategoryModel> GetCategoryBySlug(string slug);
    IDataResult<CategoryModel> UpdateCategory(CategoryModel categoryModel);
    IDataResult<CategoryModel> CreateCategory(CategoryModel categoryModel);
    IResult DeleteCategoryById(int id);
    IDataResult<List<CategoryModel>> GetAllCategoriesOrderedByName();
    IDataResult<List<CategoryModel>> GetAllCategoriesOrderedBySlug();
    IDataResult<List<CategoryModel>> GetAllFeaturedCategories();
    IDataResult<List<CategoryModel>> GetAllIndexCategories();
    
}