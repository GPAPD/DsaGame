using System.ComponentModel.DataAnnotations;

namespace ComWeb.Models
{
    public class LogInRequestDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }


    }
}
