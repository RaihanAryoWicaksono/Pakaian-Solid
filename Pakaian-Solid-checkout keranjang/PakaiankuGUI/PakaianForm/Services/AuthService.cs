using System.Threading.Tasks;
using PakaianForm.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System;

namespace PakaianForm.Services
{
    public class AuthService
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private static readonly string _baseUrl = "http://localhost:5246";

        static AuthService()
        {
            // Configure HttpClient
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public static async Task<LoginResponse> LoginAsync(User loginRequest)
        {
            try
            {
                // Create simple login request object that matches API schema
                var apiRequest = new
                {
                    username = loginRequest.Username,
                    password = loginRequest.Password
                };

                // Serialize request
                var json = JsonConvert.SerializeObject(apiRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Send request
                var response = await _httpClient.PostAsync($"{_baseUrl}/api/Auth/login", content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    // Set session
                    UserSession.CurrentUser = loginRequest.Username;

                    // Determine role based on username (temporary solution)
                    UserSession.Role = loginRequest.Username.ToLower() == "admin" ? UserRole.Admin : UserRole.Customer;

                    return new LoginResponse
                    {
                        Message = "Login berhasil",
                        Role = UserSession.Role
                    };
                }

                return new LoginResponse
                {
                    Message = $"Login gagal: {responseContent}",
                    Role = UserRole.Customer
                };
            }
            catch (HttpRequestException ex)
            {
                return new LoginResponse
                {
                    Message = $"Network Error: {ex.Message}",
                    Role = UserRole.Customer
                };
            }
            catch (TaskCanceledException)
            {
                return new LoginResponse
                {
                    Message = "Request timeout. Server tidak merespons.",
                    Role = UserRole.Customer
                };
            }
            catch (Exception ex)
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
                // Create API request object that exactly matches the schema
                var apiRequest = new
                {
                    username = registerRequest.Username,
                    password = registerRequest.Password,
                    role = (int)registerRequest.Role // Convert enum to int (0 = Customer, 1 = Admin)
                };

                // Serialize request
                var json = JsonConvert.SerializeObject(apiRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Debug: Log request
                System.Diagnostics.Debug.WriteLine($"Register Request: {json}");
                System.Diagnostics.Debug.WriteLine($"URL: {_baseUrl}/api/Auth/register");

                // Send request
                var response = await _httpClient.PostAsync($"{_baseUrl}/api/Auth/register", content);
                var responseContent = await response.Content.ReadAsStringAsync();

                // Debug: Log response
                System.Diagnostics.Debug.WriteLine($"Response Status: {response.StatusCode}");
                System.Diagnostics.Debug.WriteLine($"Response Content: {responseContent}");

                if (response.IsSuccessStatusCode)
                {
                    return "Registrasi berhasil";
                }
                else
                {
                    return $"Registrasi gagal: {responseContent}";
                }
            }
            catch (HttpRequestException ex)
            {
                return $"Network Error: {ex.Message}. Pastikan API server berjalan di http://localhost:5246";
            }
            catch (TaskCanceledException)
            {
                return "Request timeout. Server tidak merespons dalam waktu yang ditentukan.";
            }
            catch (Exception ex)
            {
                return $"Registration failed: {ex.Message}";
            }
        }

        public static async Task<bool> TestConnectionAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/swagger/index.html");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                try
                {
                    // Try alternative health check
                    var response = await _httpClient.GetAsync($"{_baseUrl}/api/Auth/all-users");
                    return true; // If we get any response, server is running
                }
                catch
                {
                    return false;
                }
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