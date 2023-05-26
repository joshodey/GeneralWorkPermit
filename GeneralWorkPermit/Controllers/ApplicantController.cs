using GeneralWorkPermit.DTO;
using GeneralWorkPermit.Models;
using GeneralWorkPermit.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GeneralWorkPermit.Controllers
{
    public class ApplicantController : Controller
    {
        private readonly UserManager<Applicants> _usermanager;
        //private readonly SignInManager<Applicants> _signin;
        private readonly IAuthManager _authManager;


        public ApplicantController(UserManager<Applicants> usermanager, IAuthManager authManager)
        {
            _usermanager = usermanager;
            _authManager = authManager;
            //_signin = signin;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserDto register)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                var mapdata = new Applicants()
                {
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                    Email = register.Email,
                    UserName = register.Email,
                    Facility = register.Facility,
                    Location = register.Location,
                    Company = register.Company,
                    Duration = register.Duration,
                    StartDate = register.StartDate,
                    EndDate = register.EndDate,
                    Equipments = register.Equipments
                };

                var result = await _usermanager.CreateAsync(mapdata, register.Password);

                if (!result.Succeeded)
                {
                    foreach (var errors in result.Errors)
                    {
                        ModelState.AddModelError(errors.Code, errors.Description);
                    }
                    //return BadRequest("User Registration failed");
                    return BadRequest(ModelState);

                }

                await _usermanager.AddToRolesAsync(mapdata, new[] { "User" });
                return Accepted("successful");
            }
            catch (Exception ex)
            {
                return Problem($"Something went wrong in {nameof(Register)}  {ex}");
            }

        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //var mapdata = _map.Map<Applicants>(user);
                //var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, false, false);
                //var testbool = await _authManager.ValidateUser(user);
                //var valuser = await _usermanager.FindByNameAsync(user.Email);

                if (!await _authManager.ValidateUser(user))
                {
                    return Unauthorized();
                }


                return Accepted(new
                {
                    Token = await _authManager.CreateToken()
                });
            }
            catch (Exception ex)
            {
                return Problem($"Something went wrong in {nameof(Login)}", statusCode: 500);
            }

        }

    }
}

