using EventManagementSystemFinal.Models;
using System.Net.Http.Json;

namespace EventManagementSystemFinal.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _apiBaseUrl;

        public RegistrationService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _apiBaseUrl = _configuration["Microservices:ApiGateway"] ?? "http://localhost:5004";
        }

        public async Task<IEnumerable<Registration>> GetUserRegistrationsAsync(string userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiBaseUrl}/api/gateway/registrations/user/{userId}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<IEnumerable<Registration>>() ?? new List<Registration>();
            }
            catch (Exception)
            {
                return new List<Registration>();
            }
        }

        public async Task<Registration?> GetRegistrationByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiBaseUrl}/api/gateway/registrations/{id}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Registration>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Registration> CreateRegistrationAsync(Registration registration)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/api/gateway/registrations", registration);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Registration>() ?? registration;
            }
            catch (Exception)
            {
                return registration;
            }
        }

        public async Task<bool> DeleteRegistrationAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/api/gateway/registrations/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
