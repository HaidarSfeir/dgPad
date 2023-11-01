using CmsShoppingCart.Payments;
using System.Collections.Generic;

namespace CmsShoppingCart.Models
{
    public class OrdesTwo
    {
        public int OrderId { get; set; }
        
        public List<Product> Products { get; set; }


        public PaymentMethod PaymentMethod { get; set; }
    }
}
