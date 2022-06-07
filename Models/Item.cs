using System.Collections.Generic;

namespace Task7FluentAPI.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public int UnitId { get; set; }
        public Unit Unit { get; set; }
        public List<OrderItem> OrderItem { get; set; }
    }
}
