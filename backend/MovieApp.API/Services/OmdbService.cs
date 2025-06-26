using System.Net.Http;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using MovieApp.API.DTOs;

namespace MovieApp.API.Services
{
    public class OmdbService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public OmdbService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["OMDB_API_KEY"] ?? throw new Exception("OMDB_API_KEY is missing in .env");
        }

        public async Task<List<MovieDto>> SearchMoviesByTitleAsync(string title)
        {
            var url = $"https://www.omdbapi.com/?apikey={_apiKey}&s={Uri.EscapeDataString(title)}";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("OMDb API request failed.");
            }

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<MovieSearchResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (result == null || result.Search == null)
                return new List<MovieDto>();

            return result.Search;
        }
    }
}
