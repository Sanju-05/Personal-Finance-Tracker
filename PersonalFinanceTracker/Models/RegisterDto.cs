using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceTracker.Models
{
    public class RegisterDto
    {
        public string? Username { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string? ConfirmPassword { get; set; }
    }
}
