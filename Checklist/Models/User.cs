using Microsoft.AspNetCore.Identity;

namespace Checklist.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
    }
}
