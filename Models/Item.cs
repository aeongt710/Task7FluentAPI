using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Task7FluentAPI.Models
{
    public class Item
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1,int.MaxValue)]
        public int Price { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
        public List<OrderItem> OrderItem { get; set; }
        public List<ItemUnit> ItemUnit { get; set; }
    }
}
