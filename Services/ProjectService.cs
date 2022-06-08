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
            return await _dbContext.Units.ToListAsync();
        }

        public async Task<string> addUnit(Unit unit)
        {
            var result = String.Empty;
            try
            {
                _dbContext.Add(unit);
                await _dbContext.SaveChangesAsync();
            }
            catch(Exception e)
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



        public async Task<IList<Item>> getItems()
        {
            return await _dbContext.Items
                .Include(a=>a.Unit).ToListAsync();
        }
        public Item getItemById(int Id)
        {
            var item = _dbContext.Items.Include(a=>a.Unit)
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
            return await _dbContext.Orders.ToListAsync();
        }

        public Order getOrderById(int Id)
        {
            var order = _dbContext.Orders.FirstOrDefault(a => a.Id == Id);
            return order;
        }

        public void deleteOrderById(int id)
        {
            _dbContext.Orders.Remove(new Order() { Id = id });
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
    }
}
