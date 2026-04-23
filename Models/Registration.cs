using System.ComponentModel.DataAnnotations;

namespace EventManagementSystemFinal.Models
{
    public enum RegistrationStatus
    {
        Confirmed = 0,
        Waitlisted = 1,
        Cancelled = 2
    }

    public class Registration
    {
        public int RegistrationId { get; set; }
        
        [Required]
        public int EventId { get; set; }
        
        [Required]
        public string UserId { get; set; } = string.Empty;
        
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        public RegistrationStatus Status { get; set; } = RegistrationStatus.Confirmed;
        public string? Notes { get; set; }
        
        // Navigation properties
        public virtual Event Event { get; set; } = null!;
        public virtual ApplicationUser User { get; set; } = null!;
    }
}
