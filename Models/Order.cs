using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Task7FluentAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public String OrderName { get; set; }
        public virtual List<OrderItem> OrderItem { get; set; }
    }
}
