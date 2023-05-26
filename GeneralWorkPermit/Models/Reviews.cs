using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneralWorkPermit.Models
{
    public class Reviews
    {
        [Key]
        public string ReviewsId { get; set; }
        public bool HazardousEnergy { get; set; }
        public bool ManagementOfChange { get; set; }
        public DateTime ByPassed { get; set; }
        public string? ByPassedDescription { get; set; }
        public int ByPassedTagNumber { get; set; }
        public string? ByPassReason { get; set; }
        public DateTime BackInService { get; set; }
        public string? BackInServiceDescription { get; set; }
        public int BackInServiceTagNumber { get; set; }
        public string? BackInServiceReason { get; set; }
        public bool ProvideAccess { get; set; }
        public bool RestrictAccess { get; set; }
        public bool CriticalLift { get; set; }
        public bool NightWork { get; set; }
        public bool JSAReviewed { get; set; }
        public string? OtherSpecialRequirements { get; set; }
        public bool SpeialRequirement { get; set; }
        public GasTestingRequireemnts? GasTesting { get; set; }
    }

    public class GasTestingRequireemnts
    {
        [Key]
        public string GasTestingId { get; set; }
        public  int Hours { get; set; }
        public int Percent { get; set; }
        public int OTwo { get; set; }
        public string? H2S { get; set; }
        public string? CoAndPpm { get; set; }
        public string? others { get; set; }
        public string? Instrument { get; set; }
        public string? GasTesterName { get; set; }
        public string? Signature { get; set; }

        [ForeignKey(nameof(Reviews))]
        public string ReviewId { get; set; }
    }
}
