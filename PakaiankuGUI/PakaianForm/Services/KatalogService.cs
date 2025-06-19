using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using PakaianForm.Models;

namespace PakaianForm.Services
{
    public class KatalogService
    {
        public static async Task<List<PakaianDto>> GetAllPakaianAsync()
        {
            try
            {
                return await ApiClient.GetAsync<List<PakaianDto>>("Katalog");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get pakaian: {ex.Message}");
            }
        }

        public static async Task<PakaianDto> GetPakaianByKodeAsync(string kode)
        {
            try
            {
                return await ApiClient.GetAsync<PakaianDto>($"Katalog/{kode}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get pakaian: {ex.Message}");
            }
        }

        public static async Task<PakaianDto> AddPakaianAsync(CreatePakaianDto createDto)
        {
            try
            {
                // Kirim userId sebagai query parameter untuk otorisasi admin
                if (UserSession.UserId == 0) throw new InvalidOperationException("User ID tidak tersedia. Harap login sebagai admin.");
                return await ApiClient.PostAsync<CreatePakaianDto, PakaianDto>($"Katalog?userId={UserSession.UserId}", createDto);
            }
            catch (Exception ex)
            {
                if (ex.InnerException is HttpRequestException httpEx)
                {
                    throw new Exception($"Gagal menambahkan pakaian: Kesalahan HTTP. Pesan: {httpEx.Message}", httpEx);
                }
                throw new Exception($"Gagal menambahkan pakaian: {ex.Message}", ex);
            }
        }

        public static async Task<PakaianDto> UpdatePakaianAsync(string kode, UpdatePakaianDto updateDto)
        {
            try
            {
                // Kirim userId sebagai query parameter untuk otorisasi admin
                if (UserSession.UserId == 0) throw new InvalidOperationException("User ID tidak tersedia. Harap login sebagai admin.");
                return await ApiClient.PutAsync<UpdatePakaianDto, PakaianDto>($"Katalog/{kode}?userId={UserSession.UserId}", updateDto);
            }
            catch (Exception ex)
            {
                if (ex.InnerException is HttpRequestException httpEx)
                {
                    throw new Exception($"Gagal memperbarui pakaian dengan kode {kode}: Kesalahan HTTP. Pesan: {httpEx.Message}", httpEx);
                }
                throw new Exception($"Gagal memperbarui pakaian dengan kode {kode}: {ex.Message}", ex);
            }
        }

        public static async Task DeletePakaianAsync(string kode)
        {
            try
            {
                // Kirim userId sebagai query parameter untuk otorisasi admin
                if (UserSession.UserId == 0) throw new InvalidOperationException("User ID tidak tersedia. Harap login sebagai admin.");
                await ApiClient.DeleteAsync($"Katalog/{kode}?userId={UserSession.UserId}");
            }
            catch (Exception ex)
            {
                if (ex.InnerException is HttpRequestException httpEx)
                {
                    throw new Exception($"Gagal menghapus pakaian dengan kode {kode}: Kesalahan HTTP. Pesan: {httpEx.Message}", httpEx);
                }
                throw new Exception($"Gagal menghapus pakaian dengan kode {kode}: {ex.Message}", ex);
            }
        }

        public static async Task<List<PakaianDto>> SearchPakaianAsync(string keyword)
        {
            try
            {
                return await ApiClient.GetAsync<List<PakaianDto>>($"Katalog/search?keyword={keyword}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to search pakaian: {ex.Message}");
            }
        }
    }
}