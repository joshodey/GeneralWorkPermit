using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace GeneralWorkPermit.Models
{
    public class Applicants
    {
        [Key]
        public string Id { get; set; } 
        public string Facility { get; set; }
        public string Location { get; set; }
        public string Company { get; set; }
        public string Duration { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Equipments { get; set; }
        public Reviews Reviews { get; set; }

        [ForeignKey(nameof(GeneralWorkPermit.Models.User))]
        public string UserId { get; set; }
        public User? User { get; set; }
    }
}
