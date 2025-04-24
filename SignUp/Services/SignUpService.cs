using Microsoft.AspNetCore.Identity;
using SignUp.Models;

namespace SignUp.Services;

public class SignUpService(UserManager<IdentityUser> userManager)
{
    private readonly UserManager<IdentityUser> _userManager = userManager;


    public async Task<ProfileRegistrationForm> RegisterUserAsync(FormData form
        )
    {
        var user = new IdentityUser
        {
            UserName = form.Email,
            Email = form.Email,

        };
        var result = await _userManager.CreateAsync(user, form.Password);


        return new ProfileRegistrationForm()
        {
            UserId = user.Id,
            FirstName = form.FirstName,
            LastName = form.LastName,
            PhoneNumber = form.PhoneNumber
        };
    }

   
}
