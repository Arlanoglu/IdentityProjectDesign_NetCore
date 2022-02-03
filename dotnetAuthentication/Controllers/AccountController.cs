using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace dotnetAuthentication.Controllers
{
    [Authorize]

    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;

        public AccountController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;

        }
        public async Task<IActionResult> Profile(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                return View(user);
            }
            return RedirectToAction("Index", "Home");
        }

    }

}