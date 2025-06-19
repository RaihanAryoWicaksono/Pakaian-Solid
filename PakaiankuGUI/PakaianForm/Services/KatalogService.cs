using System;
using System.Collections.Generic;
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