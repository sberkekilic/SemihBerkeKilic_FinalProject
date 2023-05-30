using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SemihBerkeKilic_FinalProject.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(15)]
        public string? FullName { get; set; }
        [Required]
        [StringLength(15)]
        public string Username { get; set; }
        [Required]
        [StringLength(30)]
        public string Password { get; set; }
        public bool Locked { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        public string Role { get; set; } = "user";


    }
}
