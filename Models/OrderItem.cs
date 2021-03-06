using System.ComponentModel.DataAnnotations;

namespace Task7FluentAPI.Models
{
    public class OrderItem
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        [Range(1,int.MaxValue)]
        public int Quanity { get; set; }
    }
}
