using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignUp.Models;
using SignUp.Services;

namespace SignUp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        private readonly SignUpService _signUpService;

        public SignUpController(SignUpService signUpService)
        {
            _signUpService = signUpService;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(FormData form)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!string.IsNullOrEmpty(form.FirstName) || !string.IsNullOrEmpty(form.LastName))
            {
                var result = await _signUpService.RegisterUserAsync(form);

                if (result != null) 
                {
                    var userProfile = new ProfileRegistrationForm
                    {
                        UserId = result.UserId,
                        FirstName = result.FirstName,
                        LastName = result.LastName,
                        PhoneNumber = result.PhoneNumber
                    };

                    using var http = new HttpClient();
                    var response = await http.PostAsJsonAsync("https://localhost:7147/api/profiles/create", userProfile);

                    return Ok("User registered successfully.");
                }
                else
                {
                    return BadRequest("Registration failed."); 
                }
            }

            return BadRequest("First name or last name is required.");
        }
    }
}
