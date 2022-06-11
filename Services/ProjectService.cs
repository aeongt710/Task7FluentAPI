using System.Collections.Generic;
using Task7FluentAPI.Data;
using Task7FluentAPI.Models;
using Task7FluentAPI.Services.IService;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using Task7FluentAPI.Models.VMs;

namespace Task7FluentAPI.Services
{
    public class ProjectService : IProjectService
    {
        private readonly ApplicationDbContext _dbContext;
        public ProjectService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IList<Unit>> getUnits()
        {
            return await _dbContext.Units
                .Include(a=>a.ItemUnit)
                    .ToListAsync();
        }

        public async Task<string> addUnit(Unit unit)
        {
            var result = String.Empty;
            try
            {
                _dbContext.Add(unit);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                result = e.InnerException.Message;
            }
            return result;
        }
        public Unit getUnitById(int Id)
        {
            var Unit = _dbContext.Units.FirstOrDefault(a => a.Id == Id);
            return Unit;
        }

        public void deleteUnitById(int id)
        {
            _dbContext.Units.Remove(new Unit() { Id = id });
            _dbContext.SaveChanges();
        }

        public async Task<string> updateUnit(Unit unit)
        {
            var result = String.Empty;
            try
            {
                _dbContext.Update(unit);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                result = e.InnerException.Message;
            }
            return result;
        }

        //Items

        public async Task<IList<Item>> getItems()
        {
            return await _dbContext.Items
                .Include(a => a.ItemUnit) 
                    .OrderBy(a => a.Price)
                        .Reverse().ToListAsync();
        }

        public async Task<IList<Item>> getItemsByName(string name)
        {
            return await _dbContext.Items
                .Include(a => a.ItemUnit)
                    .Include(c=>c.ItemUnit)
                        .ThenInclude(d=>d.Unit)
                            .Where(b=>(b.Name.ToLower().Contains(name.ToLower()))||(name.ToLower().Contains(b.Name.ToLower())))
                                .OrderBy(a => a.Price).Reverse().ToListAsync();
        }
        public bool addItemUnit(int itemId, int unitId)
        {
            _dbContext.ItemUnits.Add(new ItemUnit() { ItemId = itemId, UnitId = unitId });
            var result= _dbContext.SaveChanges();
            if (result > 0)
                return true;
            return false;
        }
        public bool removeItemUnit(int itemId, int unitId)
        {
            _dbContext.ItemUnits.Remove(new ItemUnit() { ItemId = itemId, UnitId = unitId });
            var result = _dbContext.SaveChanges();
            if (result > 0)
                return true;
            return false;
        }
        public async Task<IList<Item>> getItemsByUnitId(int id)
        {
            return await _dbContext.ItemUnits.Where(a => a.UnitId == id)
                .Include(b => b.Item)
                    .Include(c => c.Unit)
                        .Select(a => a.Item)
                            .ToListAsync();
        }

        public async Task<IList<Item>> getItemsByUnitIdAndName(int unitId, string name)
        {
            return await _dbContext.ItemUnits
                .Include(b => b.Item)
                    .Include(c => c.Unit)
                        .Where(a => a.UnitId == unitId && (a.Item.Name.ToLower().Contains(name.ToLower())) || (name.ToLower().Contains(a.Item.Name.ToLower())))
                            .Select(a => a.Item)
                                .ToListAsync();
        }


        public Item getItemById(int Id)
        {
            var item = _dbContext.Items
                .Include(a => a.ItemUnit)
                    .ThenInclude(b=>b.Unit)
                        .FirstOrDefault(a => a.Id == Id);
            return item;
        }
        public void deleteITemById(int id)
        {
            _dbContext.Items.Remove(new Item() { Id = id });
            _dbContext.SaveChanges();
        }

        public async Task<string> addItem(Item item)
        {
            var result = String.Empty;
            try
            {
                _dbContext.Add(item);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                result = e.InnerException.Message;
            }
            return result;
        }
        public async Task<string> updateItem(Item item)
        {
            var result = String.Empty;
            try
            {
                _dbContext.Update(item);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                result = e.InnerException.Message;
            }
            return result;
        }



        public async Task<IList<Order>> getOrders()
        {
            return await _dbContext.Orders
                .Include(a => a.OrderItem)
                    .ThenInclude(b => b.Item)
                        .ThenInclude(c => c.ItemUnit)
                            .OrderBy(a=>(a.OrderItem.Select(a=>a.Quanity*a.Item.Price).Sum()))
                                .Reverse().ToListAsync();
        }

        public Order getOrderById(int Id)
        {
            var order = _dbContext.Orders.Include(a => a.OrderItem)
                            .ThenInclude(b => b.Item)
                                .ThenInclude(c => c.ItemUnit)
                                    .ThenInclude(a=>a.Unit)
                                        .FirstOrDefault(a => a.Id == Id);
            return order;
        }

        public void deleteOrderById(int id)
        {
            var order = getOrderById(id);
            foreach(var x in order.OrderItem)
            {
                var item = x.Item;
                item.Quantity = x.Quanity + item.Quantity;
                _dbContext.Update(item);
            }
            _dbContext.Orders.Remove(order);
            _dbContext.SaveChanges();
        }

        public async Task<string> addOrder(Order order)
        {
            var result = String.Empty;
            try
            {
                _dbContext.Add(order);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                result = e.InnerException.Message;
            }
            return result;
        }

        public async Task<string> updateOrder(Order order)
        {
            var result = String.Empty;
            try
            {
                _dbContext.Update(order);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                result = e.InnerException.Message;
            }
            return result;
        }

        public IList<OrderItem> getOrderItems(int OrderId)
        {

            //    Replace With ShortHand

            //var result1 = (from orderTable in _dbContext.Orders
            //              join orderItemTable in _dbContext.OrderItems
            //                  on orderTable.Id equals orderItemTable.OrderId
            //              join itemTable in _dbContext.Items
            //                  on orderItemTable.ItemId equals itemTable.Id
            //              where orderTable.Id == OrderId 
            //              select itemTable).ToList();

            //List<Item> result2= _dbContext.Orders
            //            .Include(a => a.OrderItem)
            //                .ThenInclude(b => b.Item)
            //                    .ThenInclude(c => c.Unit)
            //                        .Where(d => d.Id == OrderId)
            //                            .Select(a=>a.OrderItem.Select(x=>x.Item).ToList())
            //                                .FirstOrDefault();

            List<OrderItem> result2 = _dbContext.Orders
                        .Include(a => a.OrderItem)
                            .ThenInclude(b => b.Item)
                                .ThenInclude(c => c.ItemUnit)
                                    .ThenInclude(d => d.Unit)
                                        .Where(d => d.Id == OrderId)
                                            .Select(a => a.OrderItem)
                                                .FirstOrDefault();
            return result2;
        }

        public void addItemToCart(int orderId, int itemId)
        {
            var updateItem = _dbContext.Items.FirstOrDefault(a => a.Id == itemId);
            var result = _dbContext.OrderItems.FirstOrDefault(a => a.OrderId == orderId && a.ItemId == itemId);
            if (result == null)
            {
                if (updateItem.Quantity > 0)
                {
                    _dbContext.Add(new OrderItem() { ItemId = itemId, OrderId = orderId, Quanity = 1 });
                    updateItem.Quantity = updateItem.Quantity - 1;
                }
            }
            else
            {
                if (updateItem.Quantity > 0)
                {
                    updateItem.Quantity = updateItem.Quantity - 1;
                    result.Quanity = result.Quanity + 1;
                    _dbContext.Update(result);
                    _dbContext.Update(updateItem);
                }
            }
            _dbContext.SaveChanges();
        }

        public void removeItemFromCart(int orderId, int itemId)
        {
           
            var result = _dbContext.OrderItems.FirstOrDefault(a => a.OrderId == orderId && a.ItemId == itemId);
            if (result != null)
            {
                var updateItem = _dbContext.Items.FirstOrDefault(a => a.Id == itemId);
                updateItem.Quantity = updateItem.Quantity + 1;
                result.Quanity = result.Quanity -1;
                if(result.Quanity==0)
                    _dbContext.Remove(result);
                else
                _dbContext.Update(result);
                _dbContext.Update(updateItem);
                _dbContext.SaveChanges();
            }
            
        }

    }
}
