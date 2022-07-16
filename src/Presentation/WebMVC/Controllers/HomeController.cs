using System.Diagnostics;
using Application.Models.Catalog;
using Application.Models.Users;
using Application.Services.Authentication.Interfaces;
using Application.Services.Catalog.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Utilities.Results;
using WebMVC.Models;

namespace WebMVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private IUserService _userService;
    private IAuthService _authService;
    private IAdSpaceService _adSpaceService;
    private ICategoryService _categoryService;
    public HomeController(ILogger<HomeController> logger, IUserService userService, IAuthService authService, IAdSpaceService adSpaceService, ICategoryService categoryService)
    {
        _logger = logger;
        _userService = userService;
        _authService = authService;
        _adSpaceService = adSpaceService;
        _categoryService = categoryService;
    }

    public IActionResult Index()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Index(CategoryModel categoryModel)
    {
        categoryModel.Visibility = true;
        categoryModel.ShowImageOnNavigation = true;
        categoryModel.CategoryOrder =1;
        categoryModel.HomepageOrder =1;
        categoryModel.FeaturedOrder =1;
        categoryModel.IsFeatured =true;
        categoryModel.ShowProductsOnIndex =true;
        
        _categoryService.CreateCategory(categoryModel);
        return View();
    }
    
    public IActionResult CreateCat()
    {
        List<IDataResult<CategoryModel>> categoryModels = new();

        List<CategoryModel> categories = new List<CategoryModel>
        {
            new CategoryModel
            {
                Title="test cat title 1",
                Description = "test category description 1",
                Keywords = "cat,ego",
                Slug = "cate-gory",
                Name = "test",
                Visibility = true,
                ShowProductsOnIndex = true,
                HomepageOrder = 2,
                IsFeatured = true,
                ShowImageOnNavigation = false,
            },
            new CategoryModel
            {
                Title="test cat title 2",
                Description = "test category description 2",
                Keywords = "cat,ego",
                Slug = "cate-gory2",
                Name = "test2",
                Visibility = false,
                ShowProductsOnIndex = false,
                HomepageOrder = 0,
                IsFeatured = true,
                ShowImageOnNavigation = false,
            },
            new CategoryModel
            {
                Title="test cat title 3",
                Description = "test category description 3",
                Keywords = "cat,ego",
                Slug = "cate-3",
                Name = "trest3",
                Visibility = false,
                ShowProductsOnIndex = false,
                HomepageOrder = 0,
                IsFeatured = false,
                ShowImageOnNavigation = false,
            },
        };

        foreach (var category in categories)
        {
            categoryModels.Add(_categoryService.CreateCategory(category));
        }
        
        return Json(categoryModels);
    }
    
    public IActionResult CreateCat1()
    {
        return Json(_categoryService.CreateCategory(new CategoryModel
        {

        Title = "error ",
            Description = "test category description 1",
            Keywords = "cat,ego",
            Slug = "cate- ewf23 **/",
            Name = "error tesr",
            Visibility = true,
            ShowProductsOnIndex = true,
            HomepageOrder = 2,
            IsFeatured = true,
            ShowImageOnNavigation = false,
        }));
    }
    
    public IActionResult GetCatFeatured()
    {
        return Json(_categoryService.GetAllFeaturedCategories());
    }
    
    public IActionResult GetCatIndex()
    {
        return Json(_categoryService.GetAllIndexCategories());
    }
    
    public IActionResult GetCatAll()
    {
        return Json(_categoryService.GetAllCategories());
    }
    
    public IActionResult GetCatSlug(string slug)
    {
        return Json(_categoryService.GetCategoryBySlug(slug));
    }
    
    public IActionResult GetAds()
    {
        return Json(_adSpaceService.GetAllAds());
    }

    public IActionResult UpdateAds()
    {
        return Json(_adSpaceService.UpdateAds(new AdSpaceModel{Name = "index_1", Code = "updated code"}));
        
    }
    
    public IActionResult UpdateAds1()
    {
        return Json(_adSpaceService.UpdateAds(new AdSpaceModel{Name = "incorrect name", Code = "updated code"}));
    }
    
    public IActionResult CreateUser()
    {
        return Json(_authService.Register(new UserRegisterModel
        {
            Agreement = true,
            Email = "saturan@gmail.com",
            Password = "sa",
            PasswordConfirm = "sa",
            LastName = "jera",
            Username = "turanheydar",
            FirstName = "sas"
        }));
    }
    
    public IActionResult Login()
    {
        return Json(_authService.Login(new UserLoginModel
        {
            Password = "sa",
            Username = "turanheydar",
        }));
    }
    
    public IActionResult Login1()
    {
        return Json(_authService.Login(new UserLoginModel
        {
            Password = "ssda",
            Username = "jeal_234",
        }));
    }
    
    
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
