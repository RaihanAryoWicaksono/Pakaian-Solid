using System;
using Newtonsoft.Json;
using PakaianForm.Models;

namespace PakaianForm.Services
{
    /// <summary>
    /// Response class untuk login API
    /// </summary>
    public class LoginResponse
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("role")]
        public UserRole Role { get; set; } = UserRole.Customer;

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        // Constructor
        public LoginResponse()
        {
            Role = UserRole.Customer;
            Success = false;
        }

        // Method untuk check apakah login berhasil
        public bool IsSuccessful()
        {
            return Success || (!string.IsNullOrEmpty(Message) && Message.Contains("berhasil"));
        }
    }

    /// <summary>
    /// Response class untuk register API
    /// </summary>
    public class RegisterResponse
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        public RegisterResponse()
        {
            Success = false;
        }
    }
}