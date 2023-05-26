using GeneralWorkPermit.DTO;
using GeneralWorkPermit.Models;

namespace GeneralWorkPermit.Services
{
    public interface IReview
    {
        Task<bool> AddInspection(ReviewDto rev, GasTestingRequireemnts gastest);
    }
}
