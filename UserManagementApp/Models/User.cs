using System.ComponentModel.DataAnnotations;
using UserManagementApp.Enums;

namespace UserManagementApp.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string PasswordHash { get; set; } = null!;
        public UserStatus Status { get; set; } = UserStatus.Unverified;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastLoginTime { get; set; }
    }
}
