using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Request.Account
{
    public class LoginDTO
    {
        [Required(ErrorMessage ="Your Email is not valid, Provide valid email"), EmailAddress]
        [RegularExpression("[^@ \\t\\r\\n]+@[^@ \\t\\r\\n]+\\.[^@ \\t\\r\\n]+")]
        [Display(Name ="Email Address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage ="Your password must be a mix of AlphaNumeric and special character")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ % ^&*-]).{8,}$")]
        public string Password { get; set; } = string.Empty;
    }
}
