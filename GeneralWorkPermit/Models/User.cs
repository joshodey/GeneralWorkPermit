using Microsoft.AspNetCore.Identity;

namespace GeneralWorkPermit.Models
{
    public enum UserType
    {
        Applicant,
        Admin
    }
    public class User : IdentityUser
    {
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserType UserType { get; set; }
        public int LoginAttempt { get; set; }
    }
}
