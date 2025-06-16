using System;

namespace PakaianForm.Models
{
    public class User
    {
        public string Username { get; set; }
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
    }

    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; } = UserRole.Customer;
    }
}