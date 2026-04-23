using EventManagementSystemFinal.Models;
using System.Net.Http.Json;

namespace EventManagementSystemFinal.Services
{
    public class EventService : IEventService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _apiBaseUrl;

        public EventService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _apiBaseUrl = _configuration["Microservices:ApiGateway"] ?? "http://localhost:5004";
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiBaseUrl}/api/gateway/events");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<IEnumerable<Event>>() ?? new List<Event>();
            }
            catch (Exception)
            {
                return new List<Event>();
            }
        }

        public async Task<Event?> GetEventByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiBaseUrl}/api/gateway/events/{id}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Event>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Event> CreateEventAsync(Event eventItem)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/api/gateway/events", eventItem);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Event>() ?? eventItem;
            }
            catch (Exception)
            {
                return eventItem;
            }
        }

        public async Task<Event?> UpdateEventAsync(int id, Event eventItem)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_apiBaseUrl}/api/gateway/events/{id}", eventItem);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Event>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteEventAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/api/gateway/events/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
