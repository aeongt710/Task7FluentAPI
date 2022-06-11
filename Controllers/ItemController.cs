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
        public async Task<IActionResult> Index(string name, int unitId)
        {
            if (unitId == 0 && name != null)
            {
                var itemIndexVM = new ItemIndexVM()
                {
                    Items = await _projectService.getItemsByName(name),
                    UnitSelectList = _projectService.getUnits().GetAwaiter().GetResult()
                        .Select(a => new SelectListItem
                        {
                            Text = a.UnitName,
                            Value = a.Id.ToString()
                        })
                };
                return View(itemIndexVM);
            }
            else if (unitId != 0 && name == null)
            {
                var itemIndexVM = new ItemIndexVM()
                {
                    Items = await _projectService.getItemsByUnitId(unitId),
                    UnitSelectList = _projectService.getUnits().GetAwaiter().GetResult()
                        .Select(a => new SelectListItem
                        {
                            Text = a.UnitName,
                            Value = a.Id.ToString()
                        })
                };
                return View(itemIndexVM);
            }
            else if (unitId != 0 && name != null)
            {
                var itemIndexVM = new ItemIndexVM()
                {
                    Items = await _projectService.getItemsByUnitIdAndName(unitId, name),
                    UnitSelectList = _projectService.getUnits().GetAwaiter().GetResult()
                        .Select(a => new SelectListItem
                        {
                            Text = a.UnitName,
                            Value = a.Id.ToString()
                        })
                };
                return View(itemIndexVM);
            }
            else
            //if (ItemIndexVM.UnitId == 0 && ItemIndexVM.Name == null)
            {


                var itemIndexVM = new ItemIndexVM()
                {
                    Items = await _projectService.getItems(),
                    UnitSelectList = _projectService.getUnits().GetAwaiter().GetResult()
                .Select(a => new SelectListItem
                {
                    Text = a.UnitName,
                    Value = a.Id.ToString()
                })
                };

                return View(itemIndexVM);
            }
        }
        [HttpPost]
        [ActionName("Index")]
        public async Task<IActionResult> SerachByName(ItemIndexVM ItemIndexVM)
        {
            //if (ItemIndexVM.UnitId == 0 && ItemIndexVM.Name != null)
            //{
            //    var itemIndexVM = new ItemIndexVM()
            //    {
            //        Items = await _projectService.getItemsByName(ItemIndexVM.Name),
            //        UnitSelectList = _projectService.getUnits().GetAwaiter().GetResult()
            //            .Select(a => new SelectListItem
            //            {
            //                Text = a.UnitName,
            //                Value = a.Id.ToString()
            //            })
            //    };
            //    return View(itemIndexVM);
            //}
            //else if (ItemIndexVM.UnitId != 0 && ItemIndexVM.Name == null)
            //{
            //    var itemIndexVM = new ItemIndexVM()
            //    {
            //        Items = await _projectService.getItemsByUnitId(ItemIndexVM.UnitId),
            //        UnitSelectList = _projectService.getUnits().GetAwaiter().GetResult()
            //            .Select(a => new SelectListItem
            //            {
            //                Text = a.UnitName,
            //                Value = a.Id.ToString()
            //            })
            //    };
            //    return View(itemIndexVM);
            //}
            //else if (ItemIndexVM.UnitId != 0 && ItemIndexVM.Name != null)
            //{
            //    var itemIndexVM = new ItemIndexVM()
            //    {
            //        Items = await _projectService.getItemsByUnitIdAndName(ItemIndexVM.UnitId, ItemIndexVM.Name),
            //        UnitSelectList = _projectService.getUnits().GetAwaiter().GetResult()
            //            .Select(a => new SelectListItem
            //            {
            //                Text = a.UnitName,
            //                Value = a.Id.ToString()
            //            })
            //    };
            //    return View(itemIndexVM);
            //}
            //else if (ItemIndexVM.UnitId == 0 && ItemIndexVM.Name == null)
            //{
            //    var itemIndexVM = new ItemIndexVM()
            //    {
            //        Items = await _projectService.getItemsByUnitIdAndName(ItemIndexVM.UnitId, string.Empty),
            //        UnitSelectList = _projectService.getUnits().GetAwaiter().GetResult()
            //            .Select(a => new SelectListItem
            //            {
            //                Text = a.UnitName,
            //                Value = a.Id.ToString()
            //            })
            //    };
            //    return View(itemIndexVM);
            //}


            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Item item)
        {
            if (ModelState.IsValid)
            {
                var result = await _projectService.addItem(item);
                if (result == "")
                    return RedirectToAction(nameof(Index));
                return Ok(result);
            }
            return View(item);
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

        public IActionResult AddUnitsToItem(int Id)
        {
            var item = _projectService.getItemById(Id);

            if (item != null)
            {
                ItemUnitVM vm = new ItemUnitVM()
                {
                    ItemId = Id,

                    UnitSelectList = _projectService.getUnits()
                        .GetAwaiter()
                            .GetResult()
                                .Where(a => !a.ItemUnit.Select(a => a.ItemId)
                                    .Contains(Id))
                        .Select(a => new SelectListItem
                        {
                            Text = a.UnitName,
                            Value = a.Id.ToString()
                        })
                };
                return View(vm);
            }
            return NotFound("Item Not Found!");
        }
        [HttpPost]
        [ActionName("AddUnitsToItem")]
        public IActionResult AddUnitsToItem(ItemUnitVM vm)
        {
            if (_projectService.addItemUnit(vm.ItemId, vm.UnitId))
            {
                return RedirectToAction(nameof(Index));
            }
            vm = new ItemUnitVM()
            {
                ItemId = vm.ItemId,
                UnitSelectList = _projectService.getUnits().GetAwaiter().GetResult()
                        .Select(a => new SelectListItem
                        {
                            Text = a.UnitName,
                            Value = a.Id.ToString()
                        })
            };
            return View(vm);
        }






        public IActionResult Edit(int Id, int unitId)
        {
            if (unitId > 0)
            {
                _projectService.removeItemUnit(Id, unitId);
            }
            var item = _projectService.getItemById(Id);
            return View(item);
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
