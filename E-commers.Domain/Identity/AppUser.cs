using Microsoft.AspNetCore.Identity;

namespace E_commers.Domain.Identity
{
    public class AppUser : IdentityUser
    {
        public string FullName {  get; set; } = string.Empty;
    }
}
