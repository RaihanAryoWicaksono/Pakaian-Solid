// PakaianForm/Models/UserDtos.cs
// FILE INI HARUS BERISI SEMUA DEFINISI DTO TERKAIT PENGGUNA DAN AUTENTIKASI.
// PASTIKAN TIDAK ADA DEFINISI DTO INI DI FILE LAIN DALAM NAMESPACE PakaianForm.Models.

using System;
using DataAnnotations = System.ComponentModel.DataAnnotations; // Alias untuk mengatasi ambiguitas Required

namespace PakaianForm.Models // PASTIKAN NAMESPACE INI BENAR
{
    public class User
    {
        public int Id { get; set; } // Sesuaikan dengan properti ID di API User Anda
        [DataAnnotations.Required]
        public string Username { get; set; }
        [DataAnnotations.Required]
        public string Password { get; set; }
        public UserRole Role { get; set; } = UserRole.Customer;
    }

    public enum UserRole
    {
        Admin,
        Customer
    }

    public class LoginResponse
    {
        public string Message { get; set; }
        public UserRole Role { get; set; }
        public int UserId { get; set; } // Pastikan ini ada jika API mengembalikannya
    }

    public class RegisterRequest
    {
        [DataAnnotations.Required]
        public string Username { get; set; }
        [DataAnnotations.Required]
        public string Password { get; set; }
        public UserRole Role { get; set; } = UserRole.Customer;
    }
}
