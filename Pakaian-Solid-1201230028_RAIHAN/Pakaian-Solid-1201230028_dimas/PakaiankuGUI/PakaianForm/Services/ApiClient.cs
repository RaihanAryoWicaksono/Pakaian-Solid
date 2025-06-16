using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Newtonsoft.Json;
using PakaianForm.Models;

namespace PakaianForm.Services
{
    public class ApiClient
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private static readonly string _baseUrl = "http://localhost:5246/api";

        static ApiClient()
        {
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        public static async Task<T> GetAsync<T>(string endpoint)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/{endpoint}");
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<T>(content);
                }

                throw new HttpRequestException($"API Error: {content}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Network Error: {ex.Message}");
            }
        }

        public static async Task<T> PostAsync<T>(string endpoint, object data)
        {
            try
            {
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_baseUrl}/{endpoint}", content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<T>(responseContent);
                }

                throw new HttpRequestException($"API Error: {responseContent}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Network Error: {ex.Message}");
            }
        }

        public static async Task<T> PutAsync<T>(string endpoint, object data)
        {
            try
            {
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"{_baseUrl}/{endpoint}", content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<T>(responseContent);
                }

                throw new HttpRequestException($"API Error: {responseContent}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Network Error: {ex.Message}");
            }
        }

        public static async Task<bool> DeleteAsync(string endpoint)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/{endpoint}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception($"Network Error: {ex.Message}");
            }
        }

        // Alternative DeleteAsync that returns data
        public static async Task<T> DeleteAsync<T>(string endpoint)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/{endpoint}");
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<T>(content);
                }

                throw new HttpRequestException($"API Error: {content}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Network Error: {ex.Message}");
            }
        }
    }
}