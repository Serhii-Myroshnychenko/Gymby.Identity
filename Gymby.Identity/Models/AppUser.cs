using Microsoft.AspNetCore.Identity;

namespace Gymby.Identity.Models
{
    public class AppUser : IdentityUser
    {
        public string Password { get; set; } = null!;
    }
}
