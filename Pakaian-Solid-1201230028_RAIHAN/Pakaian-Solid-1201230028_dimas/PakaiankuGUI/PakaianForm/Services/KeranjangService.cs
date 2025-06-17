using System;
using System.Threading.Tasks;
using PakaianForm.Models;

namespace PakaianForm.Services
{
    public class KeranjangService
    {
        public static async Task<string> AddToKeranjangAsync(AddToCartDto request)
        {
            try
            {
                return await ApiClient.PostAsync<string>("keranjang", request);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to add to cart: {ex.Message}");
            }
        }

        public static async Task<bool> RemoveFromKeranjangAsync(int index)
        {
            try
            {
                return await ApiClient.DeleteAsync($"keranjang/{index}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to remove from cart: {ex.Message}");
            }
        }

        public static async Task<KeranjangDto> GetKeranjangAsync()
        {
            try
            {
                return await ApiClient.GetAsync<KeranjangDto>("keranjang");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get cart: {ex.Message}");
            }
        }
    }
}