// Models/User.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PakaianApi.Models
{
    public class User
    {
        [Key] // Menetapkan Id sebagai Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Id akan di-generate otomatis oleh database (auto-increment)
        public int Id { get; set; } // Properti ID baru

        [Required] // Username tetap wajib diisi
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        // Enum disimpan sebagai string di database
        [Column(TypeName = "varchar(20)")]
        public UserRole Role { get; set; } = UserRole.Customer;
    }
}
