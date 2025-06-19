// Services/KeranjangService.cs - Implementasi dengan API Anda
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PakaianForm.Models;

namespace PakaianForm.Services
{
    public static class KeranjangService
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private static readonly string baseUrl = "https://localhost:7117";

        static KeranjangService()
        {
            httpClient.BaseAddress = new Uri(baseUrl);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        // GET /api/keranjang - Mendapatkan isi keranjang belanja
        public static async Task<KeranjangDto> GetKeranjangAsync()
        {
            try
            {
                if (!UserSession.IsLoggedIn || UserSession.UserId == 0) throw new InvalidOperationException("User belum login atau User ID tidak tersedia. Harap login.");
                // userId dilewatkan sebagai query parameter
                return await ApiClient.GetAsync<KeranjangDto>($"Keranjang?userId={UserSession.UserId}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal mendapatkan keranjang: {ex.Message}", ex);
            }
        }

        // POST /api/keranjang - Menambahkan pakaian ke keranjang belanja
        public static async Task<KeranjangDto> AddToKeranjangAsync(AddToCartDto request)
        {
            try
            {
                if (!UserSession.IsLoggedIn || UserSession.UserId == 0) throw new InvalidOperationException("User belum login atau User ID tidak tersedia. Harap login.");
                // userId dilewatkan sebagai query parameter
                return await ApiClient.PostAsync<AddToCartDto, KeranjangDto>($"Keranjang?userId={UserSession.UserId}", request);
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal menambahkan ke keranjang: {ex.Message}", ex);
            }
        }


        // DELETE /api/keranjang - Mengosongkan keranjang belanja
        public static async Task<bool> ClearCartAsync()
        {
            try
            {
                Console.WriteLine("Calling DELETE /api/keranjang");

                var response = await httpClient.DeleteAsync("/api/keranjang");

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Clear cart successful");
                    return true;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Clear cart failed: {response.StatusCode} - {errorContent}");
                    throw new Exception($"Gagal mengosongkan keranjang: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in ClearCartAsync: {ex.Message}");
                throw new Exception($"Gagal mengosongkan keranjang: {ex.Message}");
            }
        }

        // DELETE /api/keranjang/{index} - Menghapus pakaian dari keranjang belanja
        public static async Task<KeranjangDto> RemoveFromKeranjangAsync(int index)
        {
            try
            {
                Console.WriteLine($"Calling DELETE /api/keranjang/{index}");

                var response = await httpClient.DeleteAsync($"/api/keranjang/{index}");

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Remove item {index} successful");
                    return await ApiClient.DeleteAsync<KeranjangDto>($"Keranjang/{index}");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Remove item failed: {response.StatusCode} - {errorContent}");
                    throw new Exception($"Gagal menghapus item: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in RemoveFromKeranjangAsync: {ex.Message}");
                throw new Exception($"Gagal menghapus item dari keranjang: {ex.Message}");
            }
        }

        // POST /api/keranjang/checkout - Proses checkout keranjang belanja
        public static async Task<CheckoutResponseDto> CheckoutAsync(CheckoutDto request)
        {
            try
            {
                Console.WriteLine("Calling POST /api/keranjang/checkout");

                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync("/api/keranjang/checkout", content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Checkout successful: {jsonContent}");

                    var checkoutResponse = JsonConvert.DeserializeObject<CheckoutResponseDto>(jsonContent);
                    return checkoutResponse;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Checkout failed: {response.StatusCode} - {errorContent}");
                    throw new Exception($"Checkout gagal: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in CheckoutAsync: {ex.Message}");
                throw new Exception($"Gagal melakukan checkout: {ex.Message}");
            }
        }

        // Helper method untuk parse error message dari API
        private static string ParseApiErrorMessage(string errorContent, int statusCode)
        {
            try
            {
                // Coba parse JSON error response
                dynamic errorObj = JsonConvert.DeserializeObject(errorContent);

                if (errorObj?.message != null)
                {
                    string message = errorObj.message.ToString();

                    // Handle specific error messages
                    if (message.Contains("DalamKeranjang") || message.Contains("sudah ada"))
                    {
                        return "Item sudah ada di keranjang Anda!";
                    }
                    else if (message.Contains("stok") || message.Contains("stock"))
                    {
                        return "Stok tidak mencukupi!";
                    }
                    else if (message.Contains("tidak ditemukan") || message.Contains("not found"))
                    {
                        return "Produk tidak ditemukan!";
                    }

                    return message;
                }
            }
            catch
            {
                // Jika gagal parse JSON, gunakan raw error content
            }

            // Default error messages berdasarkan status code
            switch (statusCode)
            {
                case 400:
                    if (errorContent.ToLower().Contains("dalamkeranjang"))
                        return "Item sudah ada di keranjang Anda!";
                    return "Data yang dikirim tidak valid.";
                case 401:
                    return "Sesi Anda telah berakhir. Silakan login kembali.";
                case 403:
                    return "Anda tidak memiliki akses untuk operasi ini.";
                case 404:
                    return "Data tidak ditemukan.";
                case 500:
                    return "Terjadi kesalahan di server. Silakan coba lagi nanti.";
                default:
                    return errorContent.Length > 0 ? errorContent : "Terjadi kesalahan tidak dikenal.";
            }
        }

        // Method untuk set authentication header jika diperlukan
        public static void SetAuthToken(string token)
        {
            httpClient.DefaultRequestHeaders.Remove("Authorization");
            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            }
        }

        // Method untuk cleanup
        public static void Dispose()
        {
            httpClient?.Dispose();
        }
    }
}