using System.Diagnostics;
using GeneralWorkPermit.Context;
using GeneralWorkPermit.Models;
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
        private readonly UserManager<Applicants> _userManager;
        private readonly SignInManager<Applicants> _signin;
        private readonly ApplicationContext _context;

        public HomeController(ILogger<HomeController> logger, UserManager<Applicants> userManager, SignInManager<Applicants> signin, ApplicationContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _signin = signin;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
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

            if (login.Email.EndsWith("@gnp.com"))
            {
                var admin = await _userManager.FindByEmailAsync(login.Email);
                if (admin is null)
                {
                    var applicant = new Applicants()
                    {
                        Email = login.Email,
                        UserName = login.Email,
                        Password = login.Password,
                        Company = "Admin",
                        Duration = "Admin",
                        Equipments = "Admin",
                        EndDate = DateTime.UtcNow,
                        StartDate = DateTime.UtcNow,
                        LastName = "Admin",
                        FirstName = "Admin",
                    };

                    var result = await _userManager.CreateAsync(applicant, applicant.Password);

                    if ( !result.Succeeded)
                    {
                        return Error();
                    }

                    var roles = await _userManager.AddToRolesAsync(applicant, new[] { "Admin" });

                    admin = applicant;
                }

                return View("./Views/Dashboard/adminDashboard.cshtml", admin);

            }

            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user != null)
            {
                var result = await _signin.PasswordSignInAsync(user.UserName, login.Password, false, false);
                var log = await _userManager.CheckPasswordAsync(user, login.Password);
                if (log)
                {
                    var applicant = _context.applicant.Include(x => x.Reviews).FirstOrDefault(x => x.Id == user.Id);

                    UserDashboardDisplay dashboardDisplay = new UserDashboardDisplay()
                    {
                        Company = applicant.Company,
                        Duration = applicant.Duration,
                        Email = applicant.Email,
                        EndDate = applicant.EndDate,
                        Equipments = applicant.Equipments,
                        Facility = applicant.Facility,
                        FirstName = applicant.FirstName,
                        LastName = applicant.LastName,
                        Location = applicant.Location,
                        ReviewerName = "Admin",
                        Reviews = applicant.Reviews,
                        StartDate = applicant.StartDate,
                    };
                    

                    return View("./Views/Dashboard/userDashboard.cshtml", dashboardDisplay);
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