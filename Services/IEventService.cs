using EventManagementSystemFinal.Models;

namespace EventManagementSystemFinal.Services
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<Event?> GetEventByIdAsync(int id);
        Task<Event> CreateEventAsync(Event eventItem);
        Task<Event?> UpdateEventAsync(int id, Event eventItem);
        Task<bool> DeleteEventAsync(int id);
    }
}
