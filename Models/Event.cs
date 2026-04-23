using System.ComponentModel.DataAnnotations;

namespace EventManagementSystemFinal.Models
{
    public class Event
    {
        public int EventId { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;
        
        [Required]
        public string Description { get; set; } = string.Empty;
        
        [Required]
        [StringLength(200)]
        public string Location { get; set; } = string.Empty;
        
        [Required]
        public DateTime EventDate { get; set; }
        
        [Required]
        [Range(1, 10000)]
        public int Capacity { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
        
        public int? CategoryId { get; set; }
        
        // Navigation properties
        public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();
        public virtual Category? Category { get; set; }
    }
}
