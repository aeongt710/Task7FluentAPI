using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Task7FluentAPI.Models;
using Task7FluentAPI.Services.IService;

namespace Task7FluentAPI.Controllers
{
    public class UnitController : Controller
    {
        private readonly IProjectService _projectService;
        public UnitController(IProjectService projectService)
        {
            _projectService = projectService;
        }
        public async Task<IActionResult> Index()
        {
            var list = await _projectService.getUnits();
            return View(list);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Unit unit)
        {
            var result = await _projectService.addUnit(unit);
            if (result=="")
                return RedirectToAction(nameof(Index));
            return Ok(result);
        }
        public IActionResult Delete(int Id)
        {
            var unit=_projectService.getUnitById(Id);
            if (unit == null)
                return NotFound("Unit Not Found");
            return View(unit);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int Id)
        {
            _projectService.deleteUnitById(Id);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int Id)
        {
            var unit = _projectService.getUnitById(Id);
            return View(unit);
        }
        public IActionResult Edit(int Id)
        {
            var unit = _projectService.getUnitById(Id);
            return View(unit);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Unit unit)
        {
            if(ModelState.IsValid)
            {
                string result= await _projectService.updateUnit(unit);
                if (result != "")
                    return Ok(result);
                return RedirectToAction(nameof(Index));
            }
                
            return View(unit);
        }
    }
}
