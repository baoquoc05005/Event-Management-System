using System;
using System.ComponentModel.DataAnnotations;

namespace EventManagementSystem.Models
{
    public class Registration
    {
        public int RegistrationId { get; set; }

        [Required]
        public int EventId { get; set; }
        public Event Event { get; set; }

        [Required]
        public string UserId { get; set; }
        public User User { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        [Required]
        public RegistrationStatus Status { get; set; } = RegistrationStatus.Confirmed;
    }

    public enum RegistrationStatus
    {
        Confirmed,
        Cancelled,
        Waitlisted
    }
}
