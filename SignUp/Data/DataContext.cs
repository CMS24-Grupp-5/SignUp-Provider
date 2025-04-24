using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SignUp.Data
{
    public class DataContext(DbContextOptions options) : IdentityDbContext(options)
    {
    }
}
