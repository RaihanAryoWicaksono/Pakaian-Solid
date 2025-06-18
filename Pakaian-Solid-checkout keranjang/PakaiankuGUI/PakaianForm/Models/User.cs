using System;
using Newtonsoft.Json;

namespace PakaianForm.Models
{
    /// <summary>
    /// Simple User model untuk authentication
    /// </summary>
    public class User
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("fullName")]
        public string FullName { get; set; }

        [JsonProperty("role")]
        public UserRole Role { get; set; } = UserRole.Customer;

        [JsonProperty("isActive")]
        public bool IsActive { get; set; } = true;

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Constructor
        public User()
        {
            Role = UserRole.Customer;
            IsActive = true;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        // Simple validation
        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Username) &&
                   !string.IsNullOrWhiteSpace(Password) &&
                   Username.Length >= 3 &&
                   Password.Length >= 6;
        }

        // Method untuk display name
        public string GetDisplayName()
        {
            return !string.IsNullOrWhiteSpace(FullName) ? FullName : Username;
        }

        // Override ToString untuk debugging
        public override string ToString()
        {
            return $"User: {Username} ({Role})";
        }
    }
}