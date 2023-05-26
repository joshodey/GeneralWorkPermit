using GeneralWorkPermit.Context;
using GeneralWorkPermit.DTO;
using GeneralWorkPermit.Models;
using GeneralWorkPermit.Services;
using Microsoft.EntityFrameworkCore;

namespace GeneralWorkPermit.Implementation
{
    public class ReviewRepo : IReview
    {
        private readonly ApplicationContext _context;
        public async Task<bool> AddInspection(ReviewDto rev, GasTestingRequireemnts gastest, string applicantId)
        {
            var applicant = await _context.applicant.FindAsync(applicantId);
            
            if (applicant == null) return false;

            var review = new Reviews()
            {
                ReviewsId = applicantId,
                GasTesting = gastest,
                HazardousEnergy = rev.HazardousEnergy,
                ManagementOfChange = rev.ManagementOfChange,
                ByPassed = rev.ByPassed,
                ByPassedTagNumber = rev.ByPassedTagNumber,
                ByPassedDescription = rev.ByPassedDescription,
                ByPassReason = rev.ByPassReason,
                BackInService = rev.BackInService,
                BackInServiceDescription = rev.BackInServiceDescription,
                BackInServiceReason = rev.BackInServiceReason,
                BackInServiceTagNumber = rev.BackInServiceTagNumber,
                ProvideAccess = rev.ProvideAccess,
                RestrictAccess = rev.RestrictAccess,
                CriticalLift = rev.CriticalLift,
                NightWork = rev.NightWork,
                JSAReviewed = rev.JSAReviewed,
                OtherSpecialRequirements = rev.OtherSpecialRequirements,
                SpeialRequirement = rev.SpeialRequirement,
                GWTapprove = false,
                GWPNumber = string.Empty
            };

            await _context.reviews.AddAsync(review);
            return _context.SaveChanges() > 0;
        }

        public async Task<bool> AdminApprove(string reviewId)
        {
            var review = await _context.reviews.FindAsync(reviewId);

            review.GWTapprove = true;
            review.GWPNumber = GenerateGWPNumber();

             _context.Update(review);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<AdminApplicantDto>> GetAllApplicant()
        {
             return  _context.applicant.Select(x =>
            new AdminApplicantDto()
            {
                Company = x.Company,
                Duration = x.Duration,
                Email = x.Email,
                EndDate = x.EndDate,
                Equipments = x.Equipments,
                StartDate = x.StartDate,
                Facility = x.Facility,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Location = x.Location,
                Password = String.Empty,
                UserId = x.Id,
            }).ToList();
        }

        public async Task<List<AdminReviewDto>> GetAllReviews()
        {
            var reviews = await _context.reviews.ToListAsync();
            var reviewIds = reviews.Select(x => x.ReviewsId).ToList();

            var gastestings = await _context.gastesting
                .Where(g => reviewIds.Contains(g.ReviewId))
                .ToListAsync();


            return reviews.Select(x =>
            new AdminReviewDto()
            {
                BackInService = x.BackInService,
                BackInServiceReason = x.BackInServiceReason,
                BackInServiceDescription = x.BackInServiceDescription,
                BackInServiceTagNumber = x.BackInServiceTagNumber,
                ByPassed = x.ByPassed,
                ByPassedDescription = x.ByPassedDescription,
                ByPassedTagNumber = x.ByPassedTagNumber,
                ByPassReason = x.ByPassReason,
                CriticalLift = x.CriticalLift,
                HazardousEnergy = x.HazardousEnergy,
                JSAReviewed = x.JSAReviewed,
                ManagementOfChange = x.ManagementOfChange,
                NightWork = x.NightWork,
                OtherSpecialRequirements = x.OtherSpecialRequirements,
                ProvideAccess = x.ProvideAccess,
                SpeialRequirement = x.SpeialRequirement,
                RestrictAccess = x.RestrictAccess,
                ReviewsId = x.ReviewsId,
                gastesting = gastestings.FirstOrDefault(g => g.ReviewId == x.ReviewsId)

            }).ToList();
        }

        private string GenerateGWPNumber()
        {
            return string.Join("", Guid.NewGuid().ToString("N").Where(x => char.IsDigit(x))).Substring(0, 10);
        }

    }
}
