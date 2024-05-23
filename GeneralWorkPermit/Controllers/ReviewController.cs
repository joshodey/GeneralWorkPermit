using GeneralWorkPermit.Models;
using GeneralWorkPermit.Services;
using GeneralWorkPermit.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace GeneralWorkPermit.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReview review;
        private readonly IRepository<Applicants> _applicant;
        private readonly UserManager<User> _userManager;

        public ReviewController(IReview review, IRepository<Applicants> applicant, UserManager<User> userManager)
        {
            this.review = review;
            this._applicant = applicant;
            this._userManager = userManager;
        }

        [HttpPost("inspect-applicant")]
        public async Task<IActionResult> AddInspection([FromForm] Reviews rev, GasTestingRequireemnts gastest, string applicantId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return await review.AddInspection(rev, gastest, applicantId)? Ok("Inspection Submitted Successfully"): Problem("Error Occured");
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("get-all-reviews")]
        public async Task<IActionResult> GetAllRevies()
        {
            return Ok( await review.GetAllReviews());
        }

        [HttpGet("get-all-applicant")]
        public async Task<IActionResult> GetAllApplicant()
        {
            return Ok(await review.GetAllApplicant());
        }

        public IActionResult ViewApplicantReview(string email)
        {
            if (!email.IsNullOrEmpty())
            {
                var rev = new ReviewVM();
                rev.ApplicantEmail = email;
                
                return View("./Views/Review/review.cshtml", rev);
            }
            return View();
        }


        public async Task<IActionResult> Review(ReviewVM rev)
        {
            var user = await _userManager.FindByEmailAsync(rev.ApplicantEmail);

            var applicant = await _applicant.Get(user.Id);

            var gasTesting = new GasTestingRequireemnts()
            {
                CoAndPpm = rev.CoAndPpm,
                GasTesterName = "Admin",
                H2S = rev.H2S,
                Hours = rev.Hours,
                Instrument = rev.Instrument,
                others = rev.others,
                OTwo = rev.OTwo,
                Percent = rev.Percent,
                Signature = rev.Signature,
            };

            var review = new Reviews()
            {
                Applicant = applicant,
                ApplicantId = user.Id,
                JSAReviewed = rev.JSAReviewed,
                ProvideAccess = rev.ProvideAccess,
                RestrictAccess = rev.RestrictAccess,
                BackInService = rev.BackInService,
                BackInServiceDescription = rev.BackInServiceDescription,
                BackInServiceReason = rev.BackInServiceReason,
                BackInServiceTagNumber = rev.BackInServiceTagNumber,
                ByPassed = rev.ByPassed,
                ByPassedDescription = rev.ByPassedDescription,
                ByPassedTagNumber = rev.ByPassedTagNumber,
                ByPassReason = rev.ByPassReason,
                CriticalLift = rev.CriticalLift,
                NightWork = rev.NightWork,
                SpeialRequirement = rev.SpeialRequirement,
                GasTesting = gasTesting
            };

            var IsAdded = this.review.AddInspection(review, gasTesting, applicant.Id);

            return  Redirect("./Views/Home/index.cshtml");
        }
    }
}
