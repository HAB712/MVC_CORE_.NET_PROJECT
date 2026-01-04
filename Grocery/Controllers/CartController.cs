using Grocery.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Grocery.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly AuthDbContext db;

        public CartController(AuthDbContext db)
        {
            this.db = db;
        }

        public IActionResult AddToCart(int id)
        {
            var cart = db.Products
                         .Where(x => x.ProductId == id)
                         .ToList();

            return View(cart);
        }

        public IActionResult Checkout()
        {
            return View();
        }

    }

}
