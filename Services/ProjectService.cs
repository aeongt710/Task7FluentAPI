using System.Collections.Generic;
using Task7FluentAPI.Data;
using Task7FluentAPI.Models;
using Task7FluentAPI.Services.IService;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;

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
    }
}
