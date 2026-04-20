using EventManagementSystem.Data;
using EventManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EventManagementSystem.Controllers
{
    [Authorize]
    public class RegistrationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegistrationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Register for an event
        [HttpPost]
        public async Task<IActionResult> Register(int eventId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Check if event exists
            var ev = await _context.Events.FindAsync(eventId);
            if (ev == null)
            {
                TempData["Error"] = "Event not found";
                return RedirectToAction("Index", "Event");
            }

            // Check if already registered
            var existingRegistration = await _context.Registrations
                .FirstOrDefaultAsync(r => r.EventId == eventId && r.UserId == userId);

            if (existingRegistration != null)
            {
                TempData["Error"] = "You are already registered for this event";
                return RedirectToAction("Details", "Event", new { id = eventId });
            }

            // Check capacity
            var registrationCount = await _context.Registrations
                .CountAsync(r => r.EventId == eventId && r.Status == RegistrationStatus.Confirmed);

            var registration = new Registration
            {
                EventId = eventId,
                UserId = userId,
                RegistrationDate = DateTime.Now,
                Status = registrationCount >= ev.Capacity ? RegistrationStatus.Waitlisted : RegistrationStatus.Confirmed
            };

            _context.Registrations.Add(registration);
            await _context.SaveChangesAsync();

            if (registration.Status == RegistrationStatus.Waitlisted)
            {
                TempData["Warning"] = "Event is full. You have been added to the waitlist.";
            }
            else
            {
                TempData["Success"] = "Successfully registered for the event!";
            }

            return RedirectToAction("Details", "Event", new { id = eventId });
        }

        // View registrations
        public async Task<IActionResult> MyRegistrations()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var registrations = await _context.Registrations
                .Include(r => r.Event)
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.RegistrationDate)
                .ToListAsync();

            return View(registrations);
        }

        // Cancel registration
        [HttpPost]
        public async Task<IActionResult> Cancel(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var registration = await _context.Registrations
                .FirstOrDefaultAsync(r => r.RegistrationId == id && r.UserId == userId);

            if (registration == null)
            {
                TempData["Error"] = "Registration not found";
                return RedirectToAction("MyRegistrations");
            }

            registration.Status = RegistrationStatus.Cancelled;
            await _context.SaveChangesAsync();

            TempData["Success"] = "Registration cancelled successfully";
            return RedirectToAction("MyRegistrations");
        }
    }
}
