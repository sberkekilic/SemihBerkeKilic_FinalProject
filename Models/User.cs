using Microsoft.AspNetCore.Identity;

namespace SemihBerkeKilic_FinalProject.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
        public bool Locked { get; set; }
        public string Role { get; set; }
    }
}
