using Microsoft.AspNetCore.Mvc;
using EventManagementSystem.Data;
using EventManagementSystem.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace EventManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new AdminDashboardViewModel
            {
                TotalUsers = _context.Users.Count(),
                TotalEvents = _context.Events.Count(),
                TotalRegistrations = _context.Registrations.Count(),

                RecentUsers = _context.Users
                    .OrderByDescending(u => u.Id) // OK for now
                    .Take(5)
                    .ToList(),

                RecentEvents = _context.Events
                    .OrderByDescending(e => e.EventId) // FIXED
                    .Take(5)
                    .ToList()
            };

            return View(model);
        }
    }
}