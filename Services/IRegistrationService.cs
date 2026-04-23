using EventManagementSystemFinal.Models;

namespace EventManagementSystemFinal.Services
{
    public interface IRegistrationService
    {
        Task<IEnumerable<Registration>> GetUserRegistrationsAsync(string userId);
        Task<Registration?> GetRegistrationByIdAsync(int id);
        Task<Registration> CreateRegistrationAsync(Registration registration);
        Task<bool> DeleteRegistrationAsync(int id);
    }
}
