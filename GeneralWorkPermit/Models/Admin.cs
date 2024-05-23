using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneralWorkPermit.Models
{

    public enum AdminType
    {
        Inspector,
        Administrative
    }
    public class Admin
    {
        [Key]
        public string Id { get; set; }

        [ForeignKey(nameof(GeneralWorkPermit.Models.User))]
        public string UserId { get; set; }
        User? user { get; set; }
        public AdminType AdminType { get; set;}
    }
}
