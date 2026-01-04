using Grocery.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
                return RedirectToAction("Login");
            }
            return Content("Admin already exists in database.");
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterValidate rg)
        {
            if(!ModelState.IsValid) {
             return View(rg);
            }
            var check = db.Customers.FirstOrDefault(x => x.Email == rg.Email);
            if (check == null)
            {
                var register = new Customer
                {
                    FullName = rg.FullName,
                    Email = rg.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(rg.PasswordHash),
                    Phone = rg.Phone,
                    CreatedAt = DateTime.Now,
                    Role = "User"
                };
                db.Customers.Add(register);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            ModelState.AddModelError("Email", "Email already Exist");
            return View(rg);
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var check = db.Customers.FirstOrDefault(x => x.Email == email);
            if (check != null && BCrypt.Net.BCrypt.Verify(password, check.PasswordHash))
            {
                var stream = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, check.FullName),
                    new Claim(ClaimTypes.Email, check.Email),
                    new Claim("UserId", check.CustomerId.ToString())
                };
                var identity = new ClaimsIdentity(stream, "CookieAuth");
                HttpContext.SignInAsync("CookieAuth", new ClaimsPrincipal(identity));

                return (check.Role == "Admin") ? RedirectToAction("Index", "Admin") : RedirectToAction("Index" ,"Home");
            }

            ViewBag.Error = "Invalid Credentials";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Login");
        }


    }

}
