using System;
using System.Collections.Generic;

namespace Task7FluentAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public String OrderName { get; set; }
        public virtual List<OrderItem> OrderItem { get; set; }
    }
}
