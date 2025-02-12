using Microsoft.AspNetCore.Identity;

namespace DsaGame.BackendApi.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
