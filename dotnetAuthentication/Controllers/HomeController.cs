using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using dotnetAuthentication.Models;
using dotnetAuthentication.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace dotnetAuthentication.Controllers
{
    public class HomeController : Controller
    {


        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //Register
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser();
                user.Email = registerVM.Email;
                user.UserName = registerVM.Email;

                var result = await userManager.CreateAsync(user, registerVM.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError(err.Code, err.Description);
                    }
                    return View(registerVM);
                }
            }
            else
            {
                return View(registerVM);
            }
        }
        //Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await userManager.FindByEmailAsync(loginVM.Email);
                if (user != null)
                {
                    var result = await signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.RememberMe, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("failedLogin", "şifre hatalı");
                    }

                }
                else
                {
                    ModelState.AddModelError("notfound", "böyle bir email kayıtlı değil!");
                }


            }
            else
            {

            }
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }


        public IActionResult Denied()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
