using System.ComponentModel.DataAnnotations;

namespace ComWeb.Models
{
    public class RegistrationDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public  string Password { get; set; }

        public  string? Role { get; set; }
    }
}
