using System.ComponentModel.DataAnnotations;

namespace EventManagementSystemFinal.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
        
        // Navigation property
        public virtual ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
