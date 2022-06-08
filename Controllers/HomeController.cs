using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Task7FluentAPI.Data;
using Task7FluentAPI.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Task7FluentAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _applicationDbContext;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IActionResult> Index()
        {
            //var a = await _applicationDbContext.Orders.ToListAsync();
            //var b = await _applicationDbContext.Items.Include(a=>a.Unit).ToListAsync();
            //var c = await _applicationDbContext.OrderItems.ToListAsync();
            //var d = await _applicationDbContext.Units.ToListAsync();
            ////_applicationDbContext.Units.Remove(new Unit() { Id = 1 });
            //_applicationDbContext.SaveChanges();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
