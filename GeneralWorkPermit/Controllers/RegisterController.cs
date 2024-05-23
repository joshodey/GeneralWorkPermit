using System.Threading.Tasks;
using GeneralWorkPermit.Context;
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
        private readonly UserManager<User> _usermanager;
        private readonly SignInManager<User> _signin;
        private readonly IAuthManager _authManager;
        private readonly IEmailService _emailService;
        private readonly IRepository<Applicants> _applicantContext;
        

        public RegisterController(UserManager<User> usermanager, SignInManager<User> signin, IAuthManager authManager, IEmailService emailService, IRepository<Applicants> applicantContext)
        {
            _usermanager = usermanager;
            _signin = signin;
            _authManager = authManager;
            _emailService = emailService;
            _applicantContext = applicantContext;
            
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

                var user = new User
                {
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                    Email = register.Email,
                    Password = register.Password,
                    UserName = register.Email,
                    UserType = UserType.Applicant
                };

               
                var savedUser = await _usermanager.CreateAsync(user, register.Password);

                await _usermanager.AddToRolesAsync(user, new[] { "User" });

                user = await _usermanager.FindByEmailAsync(user.Email);


                var mapdata = new Applicants
                {
                    Id = user.Id,
                    Facility = register.Facility,
                    Location = register.Location,
                    Company = register.Company,
                    Duration = register.Duration.ToString(),
                    StartDate = register.StartDate,
                    EndDate = register.EndDate,
                    Equipments = register.Equipments,
                    UserId = user.Id,
                    User = user
                };

                var data = await _applicantContext.AddAsync(mapdata);

                if (data is null)
                    return Problem("applicant not saved");

                var emailMessage = new EmailMessage(
                    new string[] { user.Email }, 
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
