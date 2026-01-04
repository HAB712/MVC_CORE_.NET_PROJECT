using System.ComponentModel.DataAnnotations;

namespace Grocery.Models
{
    public class RegisterValidate
    {
        [Required(ErrorMessage = "Full name is required")]
        [StringLength(150)]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(150)]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).+$", ErrorMessage = "Password must contain uppercase, lowercase, number and special character")]
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; } = null!;

        [RegularExpression( @"^\+?[0-9]{10,15}$",ErrorMessage = "Phone number must contain 10 to 15 digits")]
        [StringLength(20)]
        public string? Phone { get; set; }
    }
}
