namespace EventManagementSystem.Models
{
    public class Role
    {
        public string RoleId { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
