using System.Diagnostics;
using GeneralWorkPermit.Context;
using GeneralWorkPermit.Models;
using GeneralWorkPermit.Services;
using GeneralWorkPermit.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using Org.BouncyCastle.Asn1.Pkcs;

namespace GeneralWorkPermit.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signin;
        private readonly ApplicationContext _context;
        private readonly IReview _review;
        private readonly IRepository<Applicants> _applicants;

        public HomeController(ILogger<HomeController> logger, UserManager<User> userManager, SignInManager<User> signin, ApplicationContext context, IReview review, IRepository<Applicants> applicants)
        {
            _logger = logger;
            _userManager = userManager;
            _signin = signin;
            _context = context;
            _review = review;
            _applicants = applicants;
            
        }

        public IActionResult Index()
        {
            return View( );
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //[HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _context.users.FirstOrDefault(x => x.Email == login.Email);

            if (user != null)
            {
                if (user.UserType == UserType.Applicant)
                {
                    var result = await _signin.PasswordSignInAsync(user.UserName, login.Password, false, false);
                    var log = await _userManager.CheckPasswordAsync(user, login.Password);
                    var applicant = await _context.applicant.Include(x => x.Reviews).FirstOrDefaultAsync(x => x.Id == user.Id);

                    if (applicant is null)
                    {
                        return View();
                    }
                    if (log)
                    {


                        UserDashboardDisplay dashboardDisplay = new UserDashboardDisplay()
                        {
                            Company = applicant.Company,
                            Duration = applicant.Duration,
                            Email = user.Email,
                            EndDate = applicant.EndDate,
                            Equipments = applicant.Equipments,
                            Facility = applicant.Facility,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Location = applicant.Location,
                            ReviewerName = "Admin",
                            Reviews = applicant.Reviews,
                            StartDate = applicant.StartDate,
                        };


                        return View("./Views/Dashboard/userDashboard.cshtml", dashboardDisplay);
                    }
                }
                else if (user.UserType == UserType.Admin)
                {
                    var admin = await _context.admins.FindAsync(user.Id);

                    if (admin is null)
                    {
                        return Problem("Admin does not exist");
                    }

                    var applicants = await _applicants.GetAll().Include(x => x.Reviews).Include(x => x.User).Where(x =>   x.Reviews == null || x.Reviews.GWTapprove == false).ToListAsync();

                    //var reviews = await _review.GetAllReviews();

                    var AdminDashboard = new AdminDashboardVM
                    {
                        AdminId = admin.Id,
                        AdminName = user.FirstName + " " + user.LastName,
                        ApplicantEmail = applicants.Select(x => x.User.Email).ToList(),
                    };

                    return View("./Views/Dashboard/adminDashboard.cshtml", AdminDashboard);
                }



            }

            return Problem("Invalid login attempt.");
        }
       

        
    }



     public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}