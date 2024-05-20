using Microsoft.AspNetCore.Identity;

namespace GeneralWorkPermit.Models
{
    public class Applicants : IdentityUser
    {
        public string UserId { get; set; } = Guid.NewGuid().ToString();
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Facility { get; set; }
        public string Location { get; set; }
        public string Company { get; set; }
        public string Duration { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Equipments { get; set; }
        public Reviews Reviews { get; set; }
    }
}
