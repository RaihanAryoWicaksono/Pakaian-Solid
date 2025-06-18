// PakaianForm/Services/AuthService.cs
using System.Threading.Tasks;
using PakaianForm.Models; // Untuk User, UserRole, LoginResponse, RegisterRequest
using System; // Untuk Exception
using System.Net.Http; // Untuk HttpRequestException
using System.Net; // Tambahkan ini untuk HttpStatusCode (tetapi penggunaan StatusCode pada HttpRequestException akan dihapus)

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
                }

                return response;
            }
            catch (System.Exception ex)
            {
                // Tangani kesalahan HttpRequestException di sini untuk detail lebih lanjut
                if (ex.InnerException is HttpRequestException httpEx)
                {
                    // Di .NET Framework lama, HttpRequestException.StatusCode tidak tersedia.
                    // Kita akan menyediakan pesan kesalahan yang lebih umum atau mengandalkan httpEx.Message.
                    // httpEx.Message seringkali sudah mengandung informasi status code.
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
                // Tangani kasus di mana API mengembalikan BadRequest("Username sudah terdaftar.")
                if (ex.InnerException is HttpRequestException httpEx)
                {
                    // Karena StatusCode tidak tersedia, kita tidak bisa secara langsung memeriksa BadRequest di sini.
                    // Anda mungkin perlu mengandalkan pesan httpEx.Message jika API mengembalikan detail kesalahan di dalamnya.
                    // Contoh: if (httpEx.Message.Contains("Bad Request") || httpEx.Message.Contains("sudah terdaftar"))
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
        }
    }

    public static class UserSession
    {
        public static string CurrentUser { get; set; }
        public static UserRole Role { get; set; } = UserRole.Customer;
        public static int UserId { get; set; }
        public static bool IsLoggedIn => !string.IsNullOrEmpty(CurrentUser);
        public static bool IsAdmin => Role == UserRole.Admin;
    }
}
