//User.cs Dimas
using System.ComponentModel.DataAnnotations;

namespace PakaianApi.Models
{
    public class User
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public UserRole Role { get; set; } = UserRole.Customer;
    }
}
