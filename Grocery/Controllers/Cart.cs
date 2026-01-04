using Grocery.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Grocery.Controllers
{
    [Authorize(Roles = "User")]
    public class Cart : Controller
    {
        private readonly AuthDbContext db;

        public Cart(AuthDbContext db)
        {
            this.db = db;
        }
        public IActionResult AddToCart()
        {
        return View();
        }
    }
}
