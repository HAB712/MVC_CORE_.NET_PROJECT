using Grocery.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
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

        public IActionResult AddToCart(int productId, int qty = 1)
        {
            int customerId = int.Parse(User.FindFirst("UserId").Value);

            var cart = db.Carts
                .FirstOrDefault(c => c.CustomerId == customerId );

            if (cart == null)
            {
                cart = new Cart
                {
                    CustomerId = customerId,
                    IsActive = true
                };
                db.Carts.Add(cart);
                db.SaveChanges();
            }

            var product = db.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product == null) return NotFound();

            var cartItem = db.CartItems
                .FirstOrDefault(ci => ci.CartId == cart.CartId && ci.ProductId == productId);

            if (cartItem != null)
            {
                cartItem.Quantity += qty;
            }
            else
            {
                cartItem = new CartItem
                {
                    CartId = cart.CartId,
                    ProductId = productId,
                    Quantity = qty,
                    Price = product.Price   // UNIT PRICE
                };
                db.CartItems.Add(cartItem);
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Index()
        {
           int customerId = int.Parse(User.FindFirst("UserId").Value);

            var cartItems = db.CartItems.Include(ci => ci.Product).Where(ci => ci.Cart.CustomerId == customerId).ToList();

            var vm = new HomeView
            {
                CartItems = cartItems
            };

            return View(vm);
        }
        public IActionResult Checkout()
        {
            return View();
        }

    }

}
