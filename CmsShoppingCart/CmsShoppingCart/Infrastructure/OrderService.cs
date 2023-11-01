using CmsShoppingCart.Models;
using CmsShoppingCart.Payments;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CmsShoppingCart.Infrastructure
{
    
    public class OrderService
    {
        private readonly CmsShoppingCartContext context;


        public OrderService(CmsShoppingCartContext context)
        {
            this.context = context;
        }

        /*public Order CreateOrder(PaymentMethod paymentMethod, List<Product> products)
        {
            var order = new Order
            {
                
                PaymentMethod = paymentMethod,
            };

            
            products.Products = products;

            
            context.Orders.Add(order);
            context.SaveChanges(); 

            return order;
        }*/


    }
}
