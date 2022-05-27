using System.Collections.Generic;

namespace TechApp
{
    public class CustomerCart
    {
        public string BuyerId { get; set; }

        public List<CartItem> Items { get; set; } = new();

        public CustomerCart()
        {

        }

        public CustomerCart(string customerId)
        {
            BuyerId = customerId;
        }
    }
}
