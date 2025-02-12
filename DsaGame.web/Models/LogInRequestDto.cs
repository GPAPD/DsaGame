﻿using System.ComponentModel.DataAnnotations;

namespace DsaGame.Web.Models
{
    public class LogInRequestDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }


    }
}
