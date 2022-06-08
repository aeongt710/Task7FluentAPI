using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Task7FluentAPI.Models;
using Task7FluentAPI.Models.VMs;
using Task7FluentAPI.Services.IService;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Task7FluentAPI.Controllers
{
    public class ItemController : Controller
    {
        private readonly IProjectService _projectService;
        public ItemController(IProjectService projectService)
        {
            _projectService = projectService;
        }
        public async Task<IActionResult> Index()
        {
            var list = await _projectService.getItems();
            return View(list);
        }
        public IActionResult Create()
        {
            var itemVM = new ItemVM()
            {
                UnitSelectList = _projectService.getUnits().GetAwaiter().GetResult()
                .Select(a=> new SelectListItem
                {
                    Text = a.UnitName,
                    Value = a.Id.ToString()
                })
            };
            return View(itemVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ItemVM itemVM)
        {
            if(ModelState.IsValid)
            {
                var result = await _projectService.addItem(itemVM.Item);
                if (result == "")
                    return RedirectToAction(nameof(Index));
                return Ok(result);
            }
            return View(itemVM);
        }
        public IActionResult Delete(int Id)
        {
            var item = _projectService.getItemById(Id);
            if (item == null)
                return NotFound("Item Not Found");
            return View(item);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int Id)
        {
            _projectService.deleteITemById(Id);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int Id)
        {
            var item = _projectService.getItemById(Id);
            return View(item);
        }
        public IActionResult Edit(int Id)
        {
            var itemVM = new ItemVM()
            {
                UnitSelectList = _projectService.getUnits().GetAwaiter().GetResult()
                .Select(a => new SelectListItem
                {
                    Text = a.UnitName,
                    Value = a.Id.ToString()
                }),
                Item = _projectService.getItemById(Id)
            };
            return View(itemVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ItemVM itemVm)
        {
            if (ModelState.IsValid)
            {
                string result = await _projectService.updateItem(itemVm.Item);
                if (result != "")
                    return Ok(result);
                return RedirectToAction(nameof(Index));
            }

            return View(itemVm);
        }
    }
}
