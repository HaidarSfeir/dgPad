using CmsShoppingCart.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Threading.Tasks;

namespace CmsShoppingCart.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]

    public class UsersController : Controller
    {

        private readonly UserManager<AppUser> userManager;

        public UsersController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;

        }

        public IActionResult Index()
        {
            return View(userManager.Users);
        }


        // GET: /Admin/Users/Delete/{id}
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                TempData["Error"] = "The user ID is invalid.";
            }
            else
            {
                var user = await userManager.FindByIdAsync(id);
                if (user == null)
                {
                    TempData["Error"] = "The user does not exist.";
                }
                else
                {
                    await userManager.DeleteAsync(user);
                    TempData["Success"] = "The user has been deleted.";
                }
            }

            return RedirectToAction("Index");
        }
    }
}
