using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignUp.Models;
using SignUp.Services;
using System.Net.Http;

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
                return BadRequest(ModelState);

            var result = await _signUpService.RegisterUserAsync(form);

            if (result == null)
                return BadRequest("Registration failed.");

            var userProfile = new ProfileRegistrationForm
            {
                UserId = result.UserId,
                FirstName = result.FirstName,
                LastName = result.LastName,
                PhoneNumber = result.PhoneNumber
            };

            var response = await new HttpClient().PostAsJsonAsync("https://localhost:7147/api/profiles/create", userProfile);

            if (!response.IsSuccessStatusCode)
                return StatusCode(500, "User created but profile creation failed.");

            return Ok(new { message = "User registered and profile created." });

        }

    }
}


//// här är din applicationUrl
//using var httpLogin = new HttpClient();
//var loginRespons = await httpLogin.PostAsJsonAsync(" Här skriver du din applicationUrl", loginData);
//return Ok("User registered successfully.");
