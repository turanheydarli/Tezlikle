using System.Diagnostics;
using Application.Models.Users;
using Application.Services.Authorization.Interfaces;
using Application.Services.Catalog.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Models;

namespace WebMVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private IUserService _userService;
    private IAuthService _authService;
    public HomeController(ILogger<HomeController> logger, IUserService userService, IAuthService authService)
    {
        _logger = logger;
        _userService = userService;
        _authService = authService;
    }

    public IActionResult Index()
    {
        return View();
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
