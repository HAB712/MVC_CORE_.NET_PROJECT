using Grocery.Models;
using Microsoft.AspNetCore.Mvc;

namespace Grocery.Controllers
{
    public class Admin : Controller
    {
        private readonly AuthDbContext db;

        public Admin(AuthDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Category()
        {
            return View();
        }
        public IActionResult EditCat()
        {
            return View();
        }

        public IActionResult DelCat()
        {
            return View();
        }
        public IActionResult Product()
        {
            return View();
        }

        public IActionResult EditProd()
        {
            return View();
        }

       public IActionResult DelProd()
        {
            return View();
        }
        public IActionResult Order()
        {
            return View();
        }
    }
}
