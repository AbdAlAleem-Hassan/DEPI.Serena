using Microsoft.AspNetCore.Identity;

namespace Serena.DAL.Entities
{
    public class ApplicationRole : IdentityRole
    {
        public string Description { get; set; }
    }
}
