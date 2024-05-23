using GeneralWorkPermit.Models;

namespace GeneralWorkPermit.ViewModels
{
    public class UserDashboardDisplay
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Facility { get; set; }
        public string Location { get; set; }
        public string Company { get; set; }
        public string Duration { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Equipments { get; set; }
        public Reviews? Reviews { get; set; }
        public string ReviewerName { get; set; }
        
    }
}
