using EventManagementSystem.Data;
using EventManagementSystem.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EventManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user, string password)
        {
            if (!ModelState.IsValid)
                return View(user);

            if (await _context.Users.AnyAsync(u => u.Username == user.Username))
            {
                ModelState.AddModelError("", "Username already exists");
                return View(user);
            }

            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
            {
                ModelState.AddModelError("", "Email already exists");
                return View(user);
            }
            
            user.SetPassword(password);

            user.RoleId = 1; // default user

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return RedirectToAction("Login", "Account");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
                return View();

            if (!user.VerifyPassword(password))
                return View();

            var claims = new List<Claim>
            {
                new (ClaimTypes.NameIdentifier, user.Id.ToString()),
                new (ClaimTypes.Name, user.Username),
                new (ClaimTypes.Role, user.Role.RoleName)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity));

            return RedirectToAction("Index", "Event");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}