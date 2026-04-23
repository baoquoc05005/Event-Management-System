namespace EventManagementSystemFinal.Models
{
    public class DashboardViewModel
    {
        public int TotalEvents { get; set; }
        public int UpcomingEvents { get; set; }
        public int TotalRegistrations { get; set; }
        public IEnumerable<Event> FeaturedEvents { get; set; } = new List<Event>();
    }
}
