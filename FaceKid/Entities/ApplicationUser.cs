using Microsoft.AspNetCore.Identity;

namespace FaceKid.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}
