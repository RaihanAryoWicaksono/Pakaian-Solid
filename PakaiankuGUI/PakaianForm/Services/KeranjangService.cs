// PakaianForm/Services/KeranjangService.cs
using System;
using System.Threading.Tasks;
using PakaianForm.Models; // Pastikan semua DTO yang relevan ada di sini

namespace PakaianForm.Services
{
    public static class KeranjangService // Pastikan kelas ini juga static
    {
        public static async Task<KeranjangDto> AddToKeranjangAsync(AddToCartDto request) // Mengembalikan KeranjangDto
        {
            try
            {
                // TRequest adalah AddToCartDto, TResponse adalah KeranjangDto
                return await ApiClient.PostAsync<AddToCartDto, KeranjangDto>("Keranjang", request);
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal menambahkan ke keranjang: {ex.Message}", ex); // Melempar exception asli
            }
        }

        public static async Task<KeranjangDto> RemoveFromKeranjangAsync(int index) // Mengembalikan KeranjangDto
        {
            try
            {
                // Menggunakan overload DeleteAsync yang baru untuk mengembalikan KeranjangDto
                return await ApiClient.DeleteAsync<KeranjangDto>($"Keranjang/{index}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal menghapus dari keranjang: {ex.Message}", ex);
            }
        }

        public static async Task<KeranjangDto> GetKeranjangAsync()
        {
            try
            {
                return await ApiClient.GetAsync<KeranjangDto>("Keranjang");
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal mendapatkan keranjang: {ex.Message}", ex);
            }
        }

        // Tambahkan metode untuk Checkout dan ClearCart jika Anda akan menggunakannya di UI
        public static async Task<CheckoutResponseDto> CheckoutAsync(CheckoutDto checkoutDto)
        {
            try
            {
                return await ApiClient.PostAsync<CheckoutDto, CheckoutResponseDto>("Keranjang/checkout", checkoutDto);
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal checkout: {ex.Message}", ex);
            }
        }

        public static async Task<KeranjangDto> ClearCartAsync()
        {
            try
            {
                // API ClearCart mengembalikan KeranjangDto kosong
                return await ApiClient.DeleteAsync<KeranjangDto>("Keranjang");
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal mengosongkan keranjang: {ex.Message}", ex);
            }
        }
    }
}
