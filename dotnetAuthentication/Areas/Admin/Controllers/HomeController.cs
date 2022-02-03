
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace dotnetAuthentication.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class HomeController : Controller
    {

        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public HomeController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;

        }
        public IActionResult Index()
        {
            return View(userManager.Users.ToList());
        }

        public IActionResult AddRole()
        {
            return View();
        }

        public async Task<IActionResult> AssignAdminRole(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                await userManager.AddToRoleAsync(user, "admin");
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult AssignMemberRole(string id)
        {
            return View();
        }

    }
}