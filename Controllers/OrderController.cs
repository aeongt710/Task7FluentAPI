using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Task7FluentAPI.Models;
using Task7FluentAPI.Models.VMs;
using Task7FluentAPI.Services.IService;

namespace Task7FluentAPI.Controllers
{
    public class OrderController : Controller
    {
        private readonly IProjectService _projectService;
        public OrderController(IProjectService projectService)
        {
            _projectService = projectService;
        }
        public async Task<IActionResult> Index()
        {
            var list = await _projectService.getOrders();
            return View(list);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            var result = await _projectService.addOrder(order);
            if (result == "")
                return RedirectToAction(nameof(Index));
            return Ok(result);
        }
        public IActionResult Delete(int Id)
        {
            var order = _projectService.getOrderById(Id);
            if (order == null)
                return NotFound("Order Not Found");
            return View(order);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int Id)
        {
            _projectService.deleteOrderById(Id);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int Id)
        {
            var order = _projectService.getOrderById(Id);
            return View(order);
        }
        public IActionResult Edit(int Id)
        {
            var order = _projectService.getOrderById(Id);
            return View(order);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                string result = await _projectService.updateOrder(order);
                if (result != "")
                    return Ok(result);
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }
        public async Task<IActionResult> OrderItems(int Id)
        {
            var vm = new MangeOrderItemsVM()
                {
                    Order = _projectService.getOrderById(Id),
                    AllItems = await _projectService.getItems(),
                    ItemsWithinOrder = _projectService.getOrderItems(Id)
                };
            return View(vm);
        }
        public IActionResult AddItemToCart(int orderId,int itemId)
        {
            _projectService.addItemToCart(orderId, itemId);
            return RedirectToAction(nameof(OrderItems), new { Id = orderId });
        }
        public IActionResult RemoveItemFromCart(int orderId, int itemId)
        {
            _projectService.removeItemFromCart(orderId, itemId);
            return RedirectToAction(nameof(OrderItems), new { Id = orderId });
        }
    }
}
