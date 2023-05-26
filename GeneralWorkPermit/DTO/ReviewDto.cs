namespace GeneralWorkPermit.DTO
{
    public class ReviewDto
    {
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
    }
}
