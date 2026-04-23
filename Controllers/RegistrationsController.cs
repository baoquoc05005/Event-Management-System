using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventManagementSystemFinal.Data;
using EventManagementSystemFinal.Models;
using System.Security.Claims;

namespace EventManagementSystemFinal.Controllers
{
    [Authorize]
    public class RegistrationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegistrationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var registrations = await _context.Registrations
                .Include(r => r.Event).ThenInclude(e => e.Category)
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.RegistrationDate)
                .ToListAsync();
            return View(registrations);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var registration = await _context.Registrations
                .Include(r => r.Event).ThenInclude(e => e.Category)
                .FirstOrDefaultAsync(r => r.RegistrationId == id && r.UserId == userId);
            if (registration == null) return NotFound();
            return View(registration);
        }

        public async Task<IActionResult> Create(int eventId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var eventItem = await _context.Events.Include(e => e.Registrations).FirstOrDefaultAsync(e => e.EventId == eventId && e.IsActive);
            if (eventItem == null) return NotFound();

            var existingRegistration = await _context.Registrations.FirstOrDefaultAsync(r => r.EventId == eventId && r.UserId == userId && r.Status == RegistrationStatus.Confirmed);
            if (existingRegistration != null)
            {
                TempData["Error"] = "You are already registered for this event.";
                return RedirectToAction("Details", "Events", new { id = eventId });
            }

            var confirmedRegistrations = eventItem.Registrations.Count(r => r.Status == RegistrationStatus.Confirmed);
            if (confirmedRegistrations >= eventItem.Capacity)
            {
                TempData["Error"] = "This event is full.";
                return RedirectToAction("Details", "Events", new { id = eventId });
            }

            var registration = new Registration
            {
                EventId = eventId,
                UserId = userId,
                RegistrationDate = DateTime.Now,
                Status = confirmedRegistrations >= eventItem.Capacity ? RegistrationStatus.Waitlisted : RegistrationStatus.Confirmed
            };

            return View(registration);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int EventId, string Notes)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            // Check if already registered
            var existingRegistration = await _context.Registrations
                .FirstOrDefaultAsync(r => r.EventId == EventId && r.UserId == userId);
            
            if (existingRegistration != null)
            {
                TempData["Error"] = "You are already registered for this event.";
                return RedirectToAction("Details", "Events", new { id = EventId });
            }
            
            // Get event and check capacity
            var eventItem = await _context.Events
                .Include(e => e.Registrations)
                .FirstOrDefaultAsync(e => e.EventId == EventId);
            
            if (eventItem == null)
            {
                TempData["Error"] = "Event not found.";
                return RedirectToAction("Index", "Events");
            }
            
            var confirmedRegistrations = eventItem.Registrations.Count(r => r.Status == RegistrationStatus.Confirmed);
            var status = confirmedRegistrations >= eventItem.Capacity ? RegistrationStatus.Waitlisted : RegistrationStatus.Confirmed;
            
            // Create registration
            var registration = new Registration
            {
                EventId = EventId,
                UserId = userId,
                RegistrationDate = DateTime.Now,
                Status = status,
                Notes = Notes
            };
            
            _context.Add(registration);
            await _context.SaveChangesAsync();
            
            TempData["Success"] = status == RegistrationStatus.Confirmed 
                ? "Successfully registered for the event!" 
                : "You have been added to the waitlist.";
            
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var registration = await _context.Registrations.Include(r => r.Event).FirstOrDefaultAsync(r => r.RegistrationId == id && r.UserId == userId);
            if (registration == null) return NotFound();
            return View(registration);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var registration = await _context.Registrations.FirstOrDefaultAsync(r => r.RegistrationId == id && r.UserId == userId);
            if (registration != null)
            {
                _context.Registrations.Remove(registration);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
