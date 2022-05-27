using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TechApp
{
    public class OrderStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static OrderStatus Submitted = new OrderStatus(nameof(Submitted).ToLowerInvariant());
        public static OrderStatus AwaitingValidation = new OrderStatus(nameof(AwaitingValidation).ToLowerInvariant());
        public static OrderStatus StockConfirmed = new OrderStatus(nameof(StockConfirmed).ToLowerInvariant());
        public static OrderStatus Paid = new OrderStatus(nameof(Paid).ToLowerInvariant());
        public static OrderStatus Shipped = new OrderStatus(nameof(Shipped).ToLowerInvariant());
        public static OrderStatus Cancelled = new OrderStatus(nameof(Cancelled).ToLowerInvariant());

        public OrderStatus(string name)
        {
            Name = name;
        }
        public OrderStatus()
        {
        }
    }
}
