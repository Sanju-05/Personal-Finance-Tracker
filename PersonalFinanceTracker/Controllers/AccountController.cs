using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceTracker.Data;
using PersonalFinanceTracker.Helpers;
using PersonalFinanceTracker.Models;
using PersonalFinanceTracker.Models.Entities;

namespace PersonalFinanceTracker.Controllers
{
    public class AccountController : BaseController
    {
        private readonly ApplicationDbContext applicationDb;
        private readonly JwtHelper jwtHelper;

        public AccountController(ApplicationDbContext applicationDb, JwtHelper jwtHelper) : base(jwtHelper)
        {
            this.applicationDb = applicationDb;
            this.jwtHelper = jwtHelper;
        }

        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Dashboard()
        {
            var userId = jwtHelper.GetUserIdFromCookie();
            if (userId == null)
            {
                return Unauthorized("User is not logged in.");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterDto registerDto)
        {
            if (ModelState.IsValid)
            {
                if (applicationDb.Users.Any(u => u.Email == registerDto.Email))
                {
                    ViewBag.message = "Email already exists.";
                    return View();
                }
                if (applicationDb.Users.Any(u => u.UserName == registerDto.Username))
                {
                    ViewBag.message = "Username already exists.";
                    return View();
                }
                var hashedpassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

                var user = new User
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email,
                    Password = hashedpassword
                };

                applicationDb.Users.Add(user);
                applicationDb.SaveChanges();
                ModelState.Clear();

                return RedirectToAction("Login");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginDto loginDto)
        {
            var user = applicationDb.Users.FirstOrDefault(u => u.UserName == loginDto.Username);
            if (user == null)
            {
                ViewBag.message = "Invalid username";
                return View();
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password);

            if (!isPasswordValid)
            {
                ViewBag.message = "Invalid password";
                return View();
            }
            // Generate JWT Token
            var token = jwtHelper.GenerateJwtToken(user);
            // Store in cookie or send to client (simplified version)
            Response.Cookies.Append("jwt", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddMinutes(10) // Expires in 10 Minutes
            });


            HttpContext.Session.SetString("UserLoggedIn", "true");
            HttpContext.Session.SetString("UserId", user.Id.ToString());


            return RedirectToAction("index", "home");
        }

        public IActionResult LogOut()
        {
            //HttpContext.Session.Remove("UserId");
            //HttpContext.Session.Remove("UserLoggedIn");
            // Clear cookie
            Response.Cookies.Delete("jwt");

            // Optional: clear session if you’re still using it
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

    }
}
