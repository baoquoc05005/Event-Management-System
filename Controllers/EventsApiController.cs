using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EventManagementSystemFinal.Data;
using EventManagementSystemFinal.Models;
using EventManagementSystemFinal.Services;
using Microsoft.EntityFrameworkCore;

namespace EventManagementSystemFinal.Controllers
{
    [Authorize]
    public class EventsApiController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEventService _eventService;
        private readonly ILogger<EventsApiController> _logger;

        public EventsApiController(ApplicationDbContext context, IEventService eventService, ILogger<EventsApiController> logger)
        {
            _context = context;
            _eventService = eventService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            // Try to get from microservice first, fallback to database
            var events = await _eventService.GetAllEventsAsync();
            
            if (!events.Any())
            {
                _logger.LogWarning("Microservice returned no events, falling back to database");
                events = await _context.Events
                    .Include(e => e.Category)
                    .Include(e => e.Registrations)
                    .Where(e => e.IsActive)
                    .OrderBy(e => e.EventDate)
                    .ToListAsync();
            }
            
            return View("~/Views/Events/Index.cshtml", events);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            // Try microservice first
            var eventItem = await _eventService.GetEventByIdAsync(id.Value);
            
            if (eventItem == null)
            {
                _logger.LogWarning($"Microservice failed to get event {id}, falling back to database");
                eventItem = await _context.Events
                    .Include(e => e.Category)
                    .Include(e => e.Registrations)
                    .ThenInclude(r => r.User)
                    .FirstOrDefaultAsync(e => e.EventId == id);
            }

            if (eventItem == null) return NotFound();
            return View("~/Views/Events/Details.cshtml", eventItem);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.Where(c => c.IsActive).ToList();
            return View("~/Views/Events/Create.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Title,Description,Location,EventDate,Capacity,CategoryId")] Event eventItem)
        {
            if (ModelState.IsValid)
            {
                eventItem.CreatedAt = DateTime.Now;
                eventItem.UpdatedAt = DateTime.Now;
                eventItem.IsActive = true;
                
                // Try to create via microservice
                var createdEvent = await _eventService.CreateEventAsync(eventItem);
                
                // Also save to local database for consistency
                _context.Add(eventItem);
                await _context.SaveChangesAsync();
                
                TempData["Success"] = "Event created successfully via microservice!";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = _context.Categories.Where(c => c.IsActive).ToList();
            return View("~/Views/Events/Create.cshtml", eventItem);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var eventItem = await _context.Events.FindAsync(id);
            if (eventItem == null) return NotFound();
            ViewBag.Categories = _context.Categories.Where(c => c.IsActive).ToList();
            return View("~/Views/Events/Edit.cshtml", eventItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("EventId,Title,Description,Location,EventDate,Capacity,CategoryId,IsActive")] Event eventItem)
        {
            if (id != eventItem.EventId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    eventItem.UpdatedAt = DateTime.Now;
                    
                    // Update via microservice
                    await _eventService.UpdateEventAsync(id, eventItem);
                    
                    // Also update local database
                    _context.Update(eventItem);
                    await _context.SaveChangesAsync();
                    
                    TempData["Success"] = "Event updated successfully via microservice!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(eventItem.EventId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = _context.Categories.Where(c => c.IsActive).ToList();
            return View("~/Views/Events/Edit.cshtml", eventItem);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var eventItem = await _context.Events.Include(e => e.Category).FirstOrDefaultAsync(e => e.EventId == id);
            if (eventItem == null) return NotFound();
            return View("~/Views/Events/Delete.cshtml", eventItem);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Delete via microservice
            await _eventService.DeleteEventAsync(id);
            
            // Also soft delete in local database
            var eventItem = await _context.Events.FindAsync(id);
            if (eventItem != null)
            {
                eventItem.IsActive = false;
                _context.Update(eventItem);
                await _context.SaveChangesAsync();
            }
            
            TempData["Success"] = "Event deleted successfully via microservice!";
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id) => _context.Events.Any(e => e.EventId == id);
    }
}
