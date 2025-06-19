// PakaianForm/Services/AuthService.cs
using System.Threading.Tasks;
using PakaianForm.Models;
using System;
using System.Net.Http;
using System.Net;

namespace PakaianForm.Services
{
    public class AuthService
    {
        public static async Task<LoginResponse> LoginAsync(User loginRequest)
        {
            try
            {
                var response = await ApiClient.PostAsync<User, LoginResponse>("Auth/login", loginRequest);

                if (!string.IsNullOrEmpty(response.Message) && response.Message.Contains("berhasil"))
                {
                    UserSession.CurrentUser = loginRequest.Username;
                    UserSession.Role = response.Role;
                    UserSession.UserId = response.UserId;
                    // UserSession.AuthToken = response.Token; // Dihapus
                    // ApiClient.SetAuthToken(UserSession.AuthToken); // Dihapus
                }

                return response;
            }
            catch (System.Exception ex)
            {
                if (ex.InnerException is HttpRequestException httpEx)
                {
                    return new LoginResponse
                    {
                        Message = $"Login gagal: Kesalahan komunikasi API. Pesan: {httpEx.Message}",
                        Role = UserRole.Customer,
                        UserId = 0
                    };
                }

                return new LoginResponse
                {
                    Message = $"Login gagal: {ex.Message}",
                    Role = UserRole.Customer,
                    UserId = 0
                };
            }
        }

        public static async Task<string> RegisterAsync(RegisterRequest registerRequest)
        {
            try
            {
                await ApiClient.PostAsync<RegisterRequest>("Auth/register", registerRequest);
                return "Registrasi berhasil.";
            }
            catch (System.Exception ex)
            {
                if (ex.InnerException is HttpRequestException httpEx)
                {
                    return "Registrasi gagal: Username sudah terdaftar atau data tidak valid. Pesan: " + httpEx.Message;
                }
                return $"Registrasi gagal: {ex.Message}";
            }
        }

        public static void Logout()
        {
            UserSession.CurrentUser = null;
            UserSession.Role = UserRole.Customer;
            UserSession.UserId = 0;
            // UserSession.AuthToken = null; // Dihapus
            // ApiClient.ClearAuthToken(); // Dihapus
        }
    }

    public static class UserSession
    {
        public static string CurrentUser { get; set; }
        public static UserRole Role { get; set; } = UserRole.Customer;
        public static int UserId { get; set; }
        // public static string AuthToken { get; set; } // Dihapus
        public static bool IsLoggedIn => !string.IsNullOrEmpty(CurrentUser); // Tidak memerlukan token untuk IsLoggedIn
        public static bool IsAdmin => Role == UserRole.Admin;
    }
}
