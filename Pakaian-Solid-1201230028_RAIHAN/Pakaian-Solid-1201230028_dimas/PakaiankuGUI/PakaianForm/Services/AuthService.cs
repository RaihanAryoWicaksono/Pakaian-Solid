using System.Threading.Tasks;
using PakaianForm.Models;

namespace PakaianForm.Services
{
    public class AuthService
    {
        public static async Task<LoginResponse> LoginAsync(User loginRequest)
        {
            try
            {
                var response = await ApiClient.PostAsync<LoginResponse>("auth/login", loginRequest);

                if (!string.IsNullOrEmpty(response.Message) && response.Message.Contains("berhasil"))
                {
                    UserSession.CurrentUser = loginRequest.Username;
                    UserSession.Role = response.Role;
                }

                return response;
            }
            catch (System.Exception ex)
            {
                return new LoginResponse
                {
                    Message = $"Login failed: {ex.Message}",
                    Role = UserRole.Customer
                };
            }
        }

        public static async Task<string> RegisterAsync(User registerRequest)
        {
            try
            {
                return await ApiClient.PostAsync<string>("auth/register", registerRequest);
            }
            catch (System.Exception ex)
            {
                return $"Registration failed: {ex.Message}";
            }
        }

        public static void Logout()
        {
            UserSession.CurrentUser = null;
            UserSession.Role = UserRole.Customer;
        }
    }

    public static class UserSession
    {
        public static string CurrentUser { get; set; }
        public static UserRole Role { get; set; } = UserRole.Customer;
        public static bool IsLoggedIn => !string.IsNullOrEmpty(CurrentUser);
        public static bool IsAdmin => Role == UserRole.Admin;
    }
}