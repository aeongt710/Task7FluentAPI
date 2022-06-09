using System.Collections.Generic;

namespace Task7FluentAPI.Models.VMs
{
    public class MangeOrderItemsVM
    {
        public Order Order { get; set; }
        public IList<OrderItem> ItemsWithinOrder { get; set; }
        public IList<Item> AllItems { get; set; }
    }
}
