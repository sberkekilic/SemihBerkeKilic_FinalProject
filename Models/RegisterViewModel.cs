using System.ComponentModel.DataAnnotations;

namespace SemihBerkeKilic_FinalProject.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(15)]
        public string Username { get; set; }

		[Required(ErrorMessage = "Full name is required")]
		[StringLength(15)]
		public string Fullname { get; set; }

		[Required]
        [MinLength(6)]
        [MaxLength(30)]
        public string Password { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(30)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
