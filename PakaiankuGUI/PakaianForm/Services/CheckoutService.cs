using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using PakaianForm.Models;
using System.Linq;

namespace PakaianForm.Services
{
    public static class CheckoutService
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private static readonly string baseUrl = "https://localhost:7117"; // Ganti dengan URL API Anda

        /// <summary>
        /// Process checkout dengan item keranjang
        /// </summary>
        public static async Task<bool> ProcessCheckoutAsync(List<KeranjangDto> items)
        {
            try
            {
                var checkoutRequest = new
                {
                    Items = items,
                    TotalAmount = items.Sum(x => x.TotalHarga),
                    CheckoutDate = DateTime.Now
                };

                var json = JsonConvert.SerializeObject(checkoutRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync($"{baseUrl}/checkout", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal melakukan checkout: {ex.Message}");
            }
        }
    }
}