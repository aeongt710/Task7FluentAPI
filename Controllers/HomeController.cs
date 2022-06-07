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

        public IActionResult Index()
        {
            var a = _applicationDbContext.Orders.AsNoTracking().ToList();
            var b = _applicationDbContext.Items.ToList();
            var c = _applicationDbContext.OrderItems.ToList();
            _applicationDbContext.Units.Remove(new Unit() { Id = 1 });
            _applicationDbContext.SaveChanges();
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
