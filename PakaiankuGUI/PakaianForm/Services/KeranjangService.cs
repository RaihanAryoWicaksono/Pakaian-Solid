// PakaianForm/Services/KeranjangService.cs
using System;
using System.Threading.Tasks;
using PakaianForm.Models;
using PakaianLib;

namespace PakaianForm.Services
{
    public static class KeranjangService
    {
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

        public static async Task<KeranjangDto> RemoveFromKeranjangAsync(int cartItemId) // ID item keranjang dari DB
        {
            try
            {
                if (!UserSession.IsLoggedIn || UserSession.UserId == 0) throw new InvalidOperationException("User belum login atau User ID tidak tersedia. Harap login.");
                // userId dilewatkan sebagai query parameter
                return await ApiClient.DeleteAsync<KeranjangDto>($"Keranjang/{cartItemId}?userId={UserSession.UserId}");
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
                if (!UserSession.IsLoggedIn || UserSession.UserId == 0) throw new InvalidOperationException("User belum login atau User ID tidak tersedia. Harap login.");
                // userId dilewatkan sebagai query parameter
                return await ApiClient.GetAsync<KeranjangDto>($"Keranjang?userId={UserSession.UserId}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal mendapatkan keranjang: {ex.Message}", ex);
            }
        }

        public static async Task<CheckoutResponseDto> CheckoutAsync(CheckoutDto checkoutDto)
        {
            try
            {
                if (!UserSession.IsLoggedIn || UserSession.UserId == 0) throw new InvalidOperationException("User belum login atau User ID tidak tersedia. Harap login.");
                // userId dilewatkan sebagai query parameter
                return await ApiClient.PostAsync<CheckoutDto, CheckoutResponseDto>($"Keranjang/checkout?userId={UserSession.UserId}", checkoutDto);
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
                if (!UserSession.IsLoggedIn || UserSession.UserId == 0) throw new InvalidOperationException("User belum login atau User ID tidak tersedia. Harap login.");
                // userId dilewatkan sebagai query parameter
                return await ApiClient.DeleteAsync<KeranjangDto>($"Keranjang?userId={UserSession.UserId}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal mengosongkan keranjang: {ex.Message}", ex);
            }
        }
    }
}
