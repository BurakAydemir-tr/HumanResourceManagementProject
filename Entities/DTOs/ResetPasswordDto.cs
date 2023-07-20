using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ResetPasswordDto
    {
        public int UserId { get; set; }
        [Required]
        public string ResetToken { get; set; } = string.Empty;

        [Required, MinLength(4, ErrorMessage ="En az 4 karakter giriş yapmalısınız")]
        public string Password { get; set; } = string.Empty;

        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
