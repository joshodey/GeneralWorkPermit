using GeneralWorkPermit.Models;

namespace GeneralWorkPermit.Services
{
    public interface IReview
    {
        Task<bool> AddInspection(Reviews rev, GasTestingRequireemnts gastest, string applicantId);
        Task<bool> AdminApprove(string applicantId);
        Task<List<Applicants>> GetAllApplicant();
        Task<List<Reviews>> GetAllReviews();
        Task SendPermitNumberMail(string applicantname, string email, string gwpnumber);
        Task<Applicants> GetApplicantByEmail(string email);


    }
}
