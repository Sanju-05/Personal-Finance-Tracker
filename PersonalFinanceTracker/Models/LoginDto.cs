using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceTracker.Models
{
    public class LoginDto
    {
        public string? Username { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
