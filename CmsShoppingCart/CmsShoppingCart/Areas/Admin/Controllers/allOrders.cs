using CmsShoppingCart.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CmsShoppingCart.Areas.Admin.Controllers
{
    public class allOrders : Controller
    {
        private readonly OrderService _orderService;
        public allOrders(OrderService orderService)
        {
            _orderService = orderService;
        }
        public IActionResult ViewAllOrders()
        {
            List<Order> allOrders = _orderService.GetAllOrders();
           
            return View("Index");
        }
    }
}
