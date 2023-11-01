using CmsShoppingCart.Infrastructure;
using CmsShoppingCart.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CmsShoppingCart.Controllers
{
    public class OrderController : Controller
    {
        private SignInManager<AppUser> _signInManager;
        private UserManager<AppUser> _userManager;

        private readonly CmsShoppingCartContext _context;


        public OrderController(SignInManager<AppUser> signInManager, CmsShoppingCartContext context,
            UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _context = context;
            _userManager = userManager;
        }

            public async Task<IActionResult> Index()
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var userOrders = await _context.Orders.Where(o => o.userId == userId).Include(s => s.OrderItemsIds).ToListAsync();
                return View("Index", userOrders);
            }

        public async Task<IActionResult> Delete(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItemsIds)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                TempData["Error"] = "The order does not exist";
            }
            else
            {
                

                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();

                TempData["Success"] = "The order has been deleted";
            }

            return RedirectToAction("Index");
        }





        public async Task<IActionResult> Summary(string id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItemsIds)
                .FirstOrDefaultAsync(o => o.Id.ToString().Equals(id));
            var products = await _context.Orders.Where(o => o.Id.ToString()
            .Equals(id)).Include(o => o.OrderItemsIds).ToListAsync();

            order.OrderItemsIds = products[0].OrderItemsIds;

            if (order == null)
            {
               
                return NotFound();
            }

            return View(order);
        }

    }
}
