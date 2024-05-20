using GeneralWorkPermit.Context;
using GeneralWorkPermit.EmailService;
using GeneralWorkPermit.Models;
using GeneralWorkPermit.Services;
using Microsoft.EntityFrameworkCore;

namespace GeneralWorkPermit.Implementation
{
    public class ReviewRepo : IReview
    {
        private readonly ApplicationContext _context;
        private readonly IEmailService _emailService;
        public async Task<bool> AddInspection(Reviews rev, GasTestingRequireemnts gastest, string applicantId)
        {
            var applicant = await _context.applicant.FindAsync(applicantId);
            
            if (applicant == null) return false;

            //var review = new Reviews()
            //{
            //    ReviewsId = applicantId,
            //    GasTesting = gastest,
            //    HazardousEnergy = rev.HazardousEnergy,
            //    ManagementOfChange = rev.ManagementOfChange,
            //    ByPassed = rev.ByPassed,
            //    ByPassedTagNumber = rev.ByPassedTagNumber,
            //    ByPassedDescription = rev.ByPassedDescription,
            //    ByPassReason = rev.ByPassReason,
            //    BackInService = rev.BackInService,
            //    BackInServiceDescription = rev.BackInServiceDescription,
            //    BackInServiceReason = rev.BackInServiceReason,
            //    BackInServiceTagNumber = rev.BackInServiceTagNumber,
            //    ProvideAccess = rev.ProvideAccess,
            //    RestrictAccess = rev.RestrictAccess,
            //    CriticalLift = rev.CriticalLift,
            //    NightWork = rev.NightWork,
            //    JSAReviewed = rev.JSAReviewed,
            //    OtherSpecialRequirements = rev.OtherSpecialRequirements,
            //    SpeialRequirement = rev.SpeialRequirement,
            //    GWTapprove = false,
            //    GWPNumber = string.Empty
            //};

            await _context.reviews.AddAsync(rev);
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

        public async Task<List<Applicants>> GetAllApplicant()
        {
            return _context.applicant.ToList();
        }

        public async Task<List<Reviews>> GetAllReviews()
        {
            var reviews = await _context.reviews.ToListAsync();
            var reviewIds = reviews.Select(x => x.ReviewsId).ToList();

            var gastestings = await _context.gastesting
                .Where(g => reviewIds.Contains(g.ReviewId))
                .ToListAsync();


            return reviews.ToList();
            //new AdminReviewDto()
            //{
            //    BackInService = x.BackInService,
            //    BackInServiceReason = x.BackInServiceReason,
            //    BackInServiceDescription = x.BackInServiceDescription,
            //    BackInServiceTagNumber = x.BackInServiceTagNumber,
            //    ByPassed = x.ByPassed,
            //    ByPassedDescription = x.ByPassedDescription,
            //    ByPassedTagNumber = x.ByPassedTagNumber,
            //    ByPassReason = x.ByPassReason,
            //    CriticalLift = x.CriticalLift,
            //    HazardousEnergy = x.HazardousEnergy,
            //    JSAReviewed = x.JSAReviewed,
            //    ManagementOfChange = x.ManagementOfChange,
            //    NightWork = x.NightWork,
            //    OtherSpecialRequirements = x.OtherSpecialRequirements,
            //    ProvideAccess = x.ProvideAccess,
            //    SpeialRequirement = x.SpeialRequirement,
            //    RestrictAccess = x.RestrictAccess,
            //    ReviewsId = x.ReviewsId,
            //    gastesting = gastestings.FirstOrDefault(g => g.ReviewId == x.ReviewsId)
        }

        public async Task SendPermitNumberMail( string applicantname, string email, string gwpnumber)
        {
            var mail = new EmailMessage
            (
                new List<string>() { email},
                $"Hello {applicantname}, \n\nThis is to inform you that your application is successful and your PERMIT NUMBER IS {gwpnumber}",
                "GwpNumber"
            );

           await _emailService.SendEmailAsync(mail);
        }

        public Applicants GetApplicantByEmail(string email)
        {
            return _context.Users.FirstOrDefault(x => x.Email == email); 
        }

        private static string GenerateGWPNumber()
        {
            return string.Join("", Guid.NewGuid().ToString("N").Where(x => char.IsDigit(x))).Substring(0, 10);
        }



    }
}
