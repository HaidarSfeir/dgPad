using CmsShoppingCart.Infrastructure;
using CmsShoppingCart.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CmsShoppingCart.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        private readonly CmsShoppingCartContext _context;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private IPasswordHasher<AppUser> passwordHasher;

        public AccountController(CmsShoppingCartContext context,UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IPasswordHasher<AppUser> passwordHasher)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.passwordHasher = passwordHasher;
            _context = context;

        }

        //GET /account/register
        [AllowAnonymous]
        public IActionResult Register() => View();

        //Post /account/register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public  async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid) 
            {
                AppUser appUser = new AppUser
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Address = user.Address,

                };

                IdentityResult result = await userManager.CreateAsync(appUser, user.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (IdentityError error in result.Errors) 
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }

            }
            return View(user);
        }


            //GET /account/Login
            [AllowAnonymous]
            public IActionResult Login(string returnUrl)
            {
                Login login = new Login
                {
                    ReturnUrl = returnUrl
                };
                return View(login);

            }


            //Post /account/login
            [HttpPost]
            [AllowAnonymous]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Login(Login login)
            {
                if (ModelState.IsValid)
                {
                    AppUser appUser = await userManager.FindByEmailAsync(login.Email);
                    if (appUser != null) {
                        Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.
                            PasswordSignInAsync(appUser, login.Password, false, false);
                        if (result.Succeeded)
                        {
                            return Redirect(login.ReturnUrl ?? "/");
                        }
                        ModelState.AddModelError("", "Login failed, wrong credentials. ");

                    }
                }
                return View(login);
            }


        //GET /account/Logout
        
        public async Task<IActionResult> Logout()
        {
           await signInManager.SignOutAsync();

            return Redirect("/"); 

        }

        //GET /account/edit
        public async Task<IActionResult> Edit()
        {
            AppUser appUser = await userManager.FindByNameAsync(User.Identity.Name);
            UserEdit user = new UserEdit(appUser);

            return View(user);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserEdit user)
        {
            AppUser appUser = await userManager.FindByNameAsync(User.Identity.Name);

            if (ModelState.IsValid)
            {
                appUser.Email = user.Email;
                appUser.FirstName = user.FirstName;
                appUser.LastName = user.LastName;
                appUser.UserName = user.UserName;
                appUser.Address = user.Address;

                if (!string.IsNullOrEmpty(user.Password))
                {
                    appUser.PasswordHash = passwordHasher.HashPassword(appUser, user.Password);
                }

                IdentityResult result = await userManager.UpdateAsync(appUser);
                if (result.Succeeded)
                {
                    TempData["Success"] = "Your information updated!";
                    return RedirectToAction("Index", "Home"); // Redirect to a success page or the user's profile page
                }
            }

            // If the ModelState is not valid or the update fails, return the view with the same UserEdit model.
            return View(user);
        }


        /*public async Task<IActionResult> Orders()
        {
            // Retrieve the currently logged-in user
            var user = await userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Login", new { returnUrl = Url.Action("Orders") });
            }

            // Retrieve the user's orders from your database
            // This depends on your database schema and how orders are associated with users
            var userOrders = _context.Orders.Where(o => o.Id == user.Id).ToList();

            return View("OrderHistory", userOrders);
        }*/




    }
}
