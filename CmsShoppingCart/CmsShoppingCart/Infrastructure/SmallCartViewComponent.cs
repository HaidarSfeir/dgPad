using CmsShoppingCart.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CmsShoppingCart.Infrastructure
{
    public class SmallCartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            List<CartItem> cart = HttpContext.Session.
                GetJson<List<CartItem>>("Cart");
            SmallCartViewModel smallCartVm = null;

            if(cart == null || cart.Count == 0) 
            {
                smallCartVm = null;
            }
            else
            {
                smallCartVm = new SmallCartViewModel
                {
                    NumberOtItems = cart.Sum(x => x.Quantity),
                    TotalAmount = cart.Sum(x => x.Quantity * x.Price)
                };
            }

            return View(smallCartVm); 
        }
    }
}
