using System.ComponentModel.DataAnnotations;

namespace DsaGame.web.Models
{
    public class LogInRequestDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }


    }
}
