namespace EventManagementSystem.Models.ViewModels
{
    public class AdminDashboardViewModel
    {
        public int TotalUsers { get; set; }
        public int TotalEvents { get; set; }
        public int TotalRegistrations { get; set; }

        public List<User> RecentUsers { get; set; }
        public List<Event> RecentEvents { get; set; }
    }
}