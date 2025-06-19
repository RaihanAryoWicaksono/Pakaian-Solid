// PakaianForm/Services/KatalogService.cs
using PakaianForm.Models;
using PakaianLib;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Net.Http;

namespace PakaianForm.Services
{
    public static class KatalogService
    {
        public static async Task<List<PakaianDtos>> GetAllPakaianAsync()
        {
            try
            {
                return await ApiClient.GetAsync<List<PakaianDtos>>("Katalog");
            }
            catch (Exception ex)
            {
                if (ex.InnerException is HttpRequestException httpEx)
                {
                    throw new Exception($"Gagal mendapatkan pakaian: Kesalahan HTTP. Pesan: {httpEx.Message}", httpEx);
                }
                throw new Exception($"Gagal mendapatkan pakaian: {ex.Message}", ex);
            }
        }

        public static async Task<PakaianDtos> GetPakaianByKodeAsync(string kode)
        {
            try
            {
                return await ApiClient.GetAsync<PakaianDtos>($"Katalog/{kode}");
            }
            catch (Exception ex)
            {
                if (ex.InnerException is HttpRequestException httpEx)
                {
                    throw new Exception($"Gagal mendapatkan pakaian dengan kode {kode}: Kesalahan HTTP. Pesan: {httpEx.Message}", httpEx);
                }
                throw new Exception($"Gagal mendapatkan pakaian dengan kode {kode}: {ex.Message}", ex);
            }
        }

        public static async Task<PakaianDtos> AddPakaianAsync(CreatePakaianDto createDto)
        {
            try
            {
                // Kirim userId sebagai query parameter untuk otorisasi admin
                if (UserSession.UserId == 0) throw new InvalidOperationException("User ID tidak tersedia. Harap login sebagai admin.");
                return await ApiClient.PostAsync<CreatePakaianDto, PakaianDtos>($"Katalog?userId={UserSession.UserId}", createDto);
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

        public static async Task<PakaianDtos> UpdatePakaianAsync(string kode, UpdatePakaianDto updateDto)
        {
            try
            {
                // Kirim userId sebagai query parameter untuk otorisasi admin
                if (UserSession.UserId == 0) throw new InvalidOperationException("User ID tidak tersedia. Harap login sebagai admin.");
                return await ApiClient.PutAsync<UpdatePakaianDto, PakaianDtos>($"Katalog/{kode}?userId={UserSession.UserId}", updateDto);
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

        public static async Task<PakaianDtos> ProsesAksiPakaianAsync(string kodePakaian, AksiPakaian aksi)
        {
            try
            {
                // Kirim userId sebagai query parameter untuk otorisasi admin
                if (UserSession.UserId == 0) throw new InvalidOperationException("User ID tidak tersedia. Harap login sebagai admin.");
                var prosesAksiDto = new ProsesAksiDto { KodePakaian = kodePakaian, Aksi = aksi };
                return await ApiClient.PostAsync<ProsesAksiDto, PakaianDtos>($"Katalog/aksi?userId={UserSession.UserId}", prosesAksiDto);
            }
            catch (Exception ex)
            {
                if (ex.InnerException is HttpRequestException httpEx)
                {
                    throw new Exception($"Gagal memproses aksi '{aksi}' untuk pakaian '{kodePakaian}': Kesalahan HTTP. Pesan: {httpEx.Message}", httpEx);
                }
                throw new Exception($"Gagal memproses aksi '{aksi}' untuk pakaian '{kodePakaian}': {ex.Message}", ex);
            }
        }

        public static async Task<List<PakaianDtos>> SearchPakaianAsync(string searchTerm)
        {
            try
            {
                return await ApiClient.GetAsync<List<PakaianDtos>>($"Katalog/search?keyword={searchTerm}");
            }
            catch (Exception ex)
            {
                if (ex.InnerException is HttpRequestException httpEx)
                {
                    throw new Exception($"Gagal mencari pakaian: Kesalahan HTTP. Pesan: {httpEx.Message}", httpEx);
                }
                throw new Exception($"Gagal mencari pakaian: {ex.Message}", ex);
            }
        }
    }
}
