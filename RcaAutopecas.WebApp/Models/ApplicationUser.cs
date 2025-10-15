using Microsoft.AspNetCore.Identity;

namespace RcaAutopecas.WebApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Navigation property for the seller profile
        public virtual Vendedor? Vendedor { get; set; }

        // Navigation property for the client profile
        public virtual Cliente? Cliente { get; set; }
    }
}
