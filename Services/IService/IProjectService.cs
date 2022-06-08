using System.Collections.Generic;
using System.Threading.Tasks;
using Task7FluentAPI.Models;

namespace Task7FluentAPI.Services.IService
{
    public interface IProjectService
    {
        public Task<IList<Unit>> getUnits();
        public Task<string> addUnit(Unit unit);
        public Unit getUnitById(int Id);
        public void deleteUnitById(int Id);
        public Task<string> updateUnit(Unit unit);
    }
}
