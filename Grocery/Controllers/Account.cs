using Grocery.Models;
using Microsoft.AspNetCore.Mvc;

namespace Grocery.Controllers
{
    public class Account : Controller
    {

        private readonly AuthDbContext db;

        public Account(AuthDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateAdmin()
        {
            var check = db.Customers.FirstOrDefault(x => x.Email == "admin@gmail.com");
            if (check == null)
            {
                var register = new Customer
                {
                    FullName = "Super Admin",
                    Email = "admin@gmail.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    Phone = "89856427890",
                    CreatedAt = DateTime.Now,
                    Role = "Admin"
                };

                db.Customers.Add(register);
                db.SaveChanges();
                return RedirectToAction("Register");
            }
            return Content("Admin already exists in database.");
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
    }
}
