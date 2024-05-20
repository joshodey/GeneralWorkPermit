using System.Threading.Tasks;
using GeneralWorkPermit.EmailService;
using GeneralWorkPermit.Implementation;
using GeneralWorkPermit.Models;
using GeneralWorkPermit.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace GeneralWorkPermit.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<Applicants> _usermanager;
        private readonly SignInManager<Applicants> _signin;
        private readonly IAuthManager _authManager;
        private readonly IEmailService _emailService;

        public RegisterController(UserManager<Applicants> usermanager, SignInManager<Applicants> signin, IAuthManager authManager, IEmailService emailService)
        {
            _usermanager = usermanager;
            _signin = signin;
            _authManager = authManager;
            _emailService = emailService;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(ApplicantRegistrationViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var mapdata = new Applicants
                {
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                    Email = register.Email,
                    UserName = register.Email,
                    Facility = register.Facility,
                    Location = register.Location,
                    Company = register.Company,
                    Duration = register.Duration.ToString(),
                    StartDate = register.StartDate,
                    EndDate = register.EndDate,
                    Equipments = register.Equipments,
                    Password = register.Password
                };

                var result = await _usermanager.CreateAsync(mapdata, register.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }

                await _usermanager.AddToRolesAsync(mapdata, new[] { "User" });

                var emailMessage = new EmailMessage(
                    new string[] { mapdata.Email }, 
                    "General Work Permit", 
                    "Registration was successful, please wait for inspection."
                );
                await _emailService.SendEmailAsync(emailMessage);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return Problem($"Something went wrong in {nameof(Register)}: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _usermanager.FindByEmailAsync(login.Email);
            if (user != null)
            {
                var result = await _signin.PasswordSignInAsync(user.UserName, login.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return Problem("Invalid login attempt.");
        }
    }

    public class ApplicantRegistrationViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Facility { get; set; }
        public string Location { get; set; }
        public string Company { get; set; }
        public int Duration { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Equipments { get; set; }
        public string Password { get; set; }
    }
}
