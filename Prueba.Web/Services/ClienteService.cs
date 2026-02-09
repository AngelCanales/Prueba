namespace Prueba.Web.Services
{
    using Prueba.Web.Models;
    using System.Net.Http.Json;

    public class ClienteService
    {
        private readonly HttpClient _http;

        public ClienteService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<ClienteModel>> GetAllAsync()
        {
            var response = await _http.GetFromJsonAsync<ApiResponse<List<ClienteModel>>>("api/clientes");
            return response?.Data ?? new List<ClienteModel>();
        }

        public async Task<ClienteModel?> GetByIdAsync(int id)
        {
            var response = await _http.GetFromJsonAsync<ApiResponse<ClienteModel>>($"api/clientes/{id}");
            return response?.Data;
        }

        public async Task<bool> CreateAsync(ClienteModel cliente)
        {
            var response = await _http.PostAsJsonAsync("api/clientes", cliente);
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<ClienteModel>>();
            return result?.Success ?? false;
        }

        public async Task<bool> UpdateAsync(ClienteModel cliente)
        {
            var response = await _http.PutAsJsonAsync($"api/clientes/{cliente.ClienteId}", cliente);
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<ClienteModel>>();
            return result?.Success ?? false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/clientes/{id}");
            return response.IsSuccessStatusCode;
        }
    }

}
