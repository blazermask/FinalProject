using FinalProject.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using FinalProject.Services;
namespace FinalProject.Controllers;


public class UserController : Controller
{
    private readonly IUserService userService;
    public UserController(IUserService userService)
    {
        this.userService = userService;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();

    }
    [HttpPost]
    public IActionResult Register(CreateUserDto userDto)
    {
        this.userService.Create(userDto);
        return RedirectToAction("Login",controllerName: "User");
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Login(LoginUserDto user)
    {
        this.userService.Login(user);
        return RedirectToAction("Index", controllerName: "Home");
    }

    [HttpGet]
    public IActionResult Logout()
    {
        this.userService.Logout();
        return RedirectToAction("Index", "Home");
    }


}




