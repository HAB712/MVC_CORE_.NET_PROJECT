using Grocery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Grocery.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private readonly AuthDbContext db;

        public HomeController(AuthDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            var prod_show = new HomeView
            {
                Product = db.Products.Include(x => x.Category).OrderBy(p => p.ProductId).ToList(),
                Category = db.Categories.ToList()
            };
            
            return View(prod_show);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Prodlist(int id)
        {
            var check = db.Products.Where(p=> p.ProductId == id).Include(x => x.Category).ToList();

            var model = new HomeView
            {
                Product = check
            };
            return View(model);
        }
        public IActionResult SingleProd()
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
