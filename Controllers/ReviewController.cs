using GeneralWorkPermit.Models;
using GeneralWorkPermit.Services;
using Microsoft.AspNetCore.Mvc;

namespace GeneralWorkPermit.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReview review;

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

        public IActionResult ViewApplicantReview(Applicants applicants)
        {
            return Ok();
        }
    }
}
