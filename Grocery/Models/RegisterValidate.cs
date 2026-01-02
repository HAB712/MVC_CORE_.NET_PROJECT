using System.ComponentModel.DataAnnotations;

namespace Grocery.Models
{
    public class RegisterValidate
    {
        [StringLength(150)]
        public string FullName { get; set; } = null!;

        [StringLength(150)]
        public string Email { get; set; } = null!;

        [StringLength(255)]
        public string PasswordHash { get; set; } = null!;

        [StringLength(20)]
        public string? Phone { get; set; }
    }
}
