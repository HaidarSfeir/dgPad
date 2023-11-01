using CmsShoppingCart.Infrastructure;
using CmsShoppingCart.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CmsShoppingCart.Areas.Admin.Controllers
{
    public class OrderService
    {
        private readonly CmsShoppingCartContext _context;

        public OrderService(CmsShoppingCartContext context) { 
        _context = context;
        }

        public List<Order> GetAllOrders()
        {
            // Use Include method to include related OrderItems if necessary
            return _context.Orders
                .Include(o => o.OrderItemsIds)
                .ToList();
        }

    }
}
