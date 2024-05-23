using GeneralWorkPermit.Models;

namespace GeneralWorkPermit.ViewModels
{
    public class AdminDashboardVM
    {
        public string AdminId { get; set; }
        public string AdminName { get; set; }
        public List<Reviews> Reviews { get; set; }
        public List<string> ApplicantEmail {get; set; }
        public string OutputEmail { get; set; }
    }
}
