using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventManagementSystemFinal.Data;
using EventManagementSystemFinal.Models;

namespace EventManagementSystemFinal.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var events = await _context.Events
                .Include(e => e.Category)
                .Include(e => e.Registrations)
                .Where(e => e.IsActive)
                .OrderBy(e => e.EventDate)
                .ToListAsync();
            return View(events);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var eventItem = await _context.Events
                .Include(e => e.Category)
                .Include(e => e.Registrations)
                .ThenInclude(r => r.User)
                .FirstOrDefaultAsync(e => e.EventId == id);

            if (eventItem == null) return NotFound();
            return View(eventItem);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.Where(c => c.IsActive).ToList();
            return View();
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
                _context.Add(eventItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = _context.Categories.Where(c => c.IsActive).ToList();
            return View(eventItem);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var eventItem = await _context.Events.FindAsync(id);
            if (eventItem == null) return NotFound();
            ViewBag.Categories = _context.Categories.Where(c => c.IsActive).ToList();
            return View(eventItem);
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
                    _context.Update(eventItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(eventItem.EventId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = _context.Categories.Where(c => c.IsActive).ToList();
            return View(eventItem);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var eventItem = await _context.Events.Include(e => e.Category).FirstOrDefaultAsync(e => e.EventId == id);
            if (eventItem == null) return NotFound();
            return View(eventItem);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventItem = await _context.Events.FindAsync(id);
            if (eventItem != null)
            {
                eventItem.IsActive = false;
                _context.Update(eventItem);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id) => _context.Events.Any(e => e.EventId == id);
    }
}
