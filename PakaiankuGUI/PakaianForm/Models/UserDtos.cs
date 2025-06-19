// PakaianForm/Models/UserDtos.cs
using System;
using DataAnnotations = System.ComponentModel.DataAnnotations;

namespace PakaianForm.Models
{
    public class User
    {
        public int Id { get; set; }
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
        public int UserId { get; set; }
        // public string Token { get; set; } // Hapus properti token
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
