using System.Collections.Generic;
using System.Threading.Tasks;
using Task7FluentAPI.Models;
using Task7FluentAPI.Models.VMs;

namespace Task7FluentAPI.Services.IService
{
    public interface IProjectService
    {
        public Task<IList<Unit>> getUnits();
        public Task<string> addUnit(Unit unit);
        public Unit getUnitById(int Id);
        public void deleteUnitById(int Id);
        public Task<string> updateUnit(Unit unit);


        public Task<IList<Item>> getItems();
        public Task<IList<Item>> getItemsByName(string name);
        public Task<IList<Item>> getItemsByUnitId(int id);
        public Task<IList<Item>> getItemsByUnitIdAndName(int unitId,string name);
        public Item getItemById(int Id);
        public void deleteITemById(int id);
        public Task<string> addItem(Item item);
        public Task<string> updateItem(Item item);


        public Task<IList<Order>> getOrders();
        public Order getOrderById(int Id);
        public void deleteOrderById(int id);
        public Task<string> addOrder(Order order);
        public Task<string> updateOrder(Order order);


        public IList<OrderItem> getOrderItems(int OrderId);
        public void addItemToCart(int orderId, int itemId);
        public void removeItemFromCart(int orderId, int itemId);
    }
}
