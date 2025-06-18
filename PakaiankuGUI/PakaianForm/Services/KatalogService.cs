// PakaianForm/Services/KatalogService.cs
using PakaianForm.Models; // Untuk PakaianDtos
using PakaianLib; // Untuk StatusPakaian, AksiPakaian
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Net.Http;

namespace PakaianForm.Services
{
    public static class KatalogService
    {
        public static async Task<List<PakaianDtos>> GetAllPakaianAsync() // <--- Menggunakan PakaianDtos
        {
            try
            {
                return await ApiClient.GetAsync<List<PakaianDtos>>("Katalog"); // <--- Menggunakan PakaianDtos
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

        public static async Task<PakaianDtos> GetPakaianByKodeAsync(string kode) // <--- Menggunakan PakaianDtos
        {
            try
            {
                return await ApiClient.GetAsync<PakaianDtos>($"Katalog/{kode}"); // <--- Menggunakan PakaianDtos
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

        public static async Task<PakaianDtos> AddPakaianAsync(CreatePakaianDto createDto, string username) // <--- Menggunakan PakaianDtos
        {
            try
            {
                return await ApiClient.PostAsync<CreatePakaianDto, PakaianDtos>($"Katalog?username={username}", createDto); // <--- Menggunakan PakaianDtos
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

        public static async Task<PakaianDtos> UpdatePakaianAsync(string kode, UpdatePakaianDto updateDto) // <--- Menggunakan PakaianDtos
        {
            try
            {
                return await ApiClient.PutAsync<UpdatePakaianDto, PakaianDtos>($"Katalog/{kode}", updateDto); // <--- Menggunakan PakaianDtos
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
                await ApiClient.DeleteAsync($"Katalog/{kode}");
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

        public static async Task<PakaianDtos> ProsesAksiPakaianAsync(string kodePakaian, AksiPakaian aksi) // <--- Menggunakan PakaianDtos
        {
            try
            {
                var prosesAksiDto = new ProsesAksiDto { KodePakaian = kodePakaian, Aksi = aksi };
                return await ApiClient.PostAsync<ProsesAksiDto, PakaianDtos>("Katalog/aksi", prosesAksiDto); // <--- Menggunakan PakaianDtos
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

        public static async Task<List<PakaianDtos>> SearchPakaianAsync(string searchTerm) // <--- Menggunakan PakaianDtos
        {
            try
            {
                return await ApiClient.GetAsync<List<PakaianDtos>>($"Katalog/search?keyword={searchTerm}"); // <--- Menggunakan PakaianDtos
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
