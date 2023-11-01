using System.Collections.Generic;

namespace CmsShoppingCart.Models
{
    public class savedOrder
    {
        public int Id { get; set; }
        public int CartItemId { get; set; } // This will be a foreign key referencing CartItem
        public int Quantity { get; set; }

        // Other properties related to the order history

        // Navigation property to access the associated CartItem
        public CartItem CartItem { get; set; }
    }
}
