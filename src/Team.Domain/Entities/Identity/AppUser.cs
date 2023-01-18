using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Team.Domain.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }
    }
}
