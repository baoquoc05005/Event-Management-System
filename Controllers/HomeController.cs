using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventManagementSystemFinal.Data;
using EventManagementSystemFinal.Models;

namespace EventManagementSystemFinal.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var events = await _context.Events
            .Include(e => e.Category)
            .Include(e => e.Registrations)
            .Where(e => e.IsActive && e.EventDate >= DateTime.Now)
            .OrderBy(e => e.EventDate)
            .Take(6)
            .ToListAsync();

        var stats = new DashboardViewModel
        {
            TotalEvents = await _context.Events.CountAsync(e => e.IsActive),
            UpcomingEvents = await _context.Events.CountAsync(e => e.IsActive && e.EventDate >= DateTime.Now),
            TotalRegistrations = await _context.Registrations.CountAsync(r => r.Status == RegistrationStatus.Confirmed),
            FeaturedEvents = events
        };

        return View(stats);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
