using MovieApp.WPF.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using MovieApp.WPF.Helpers;
using System.Text;
using System.Diagnostics;

namespace MovieApp.WPF.Services;
public static class MovieService
{
    public static HttpClient GetClientWithAuth()
    {
        var client = new HttpClient();

        if (!string.IsNullOrWhiteSpace(AuthSession.Token))
        {
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", AuthSession.Token);
        }
        Debug.WriteLine("Token actual: " + AuthSession.Token);

        return client;
    }



    public static async Task<List<MovieModel>> SearchMoviesAsync(string title)
    {
        var client = GetClientWithAuth();
        var response = await client.GetAsync($"http://localhost:5271/api/Movies?title={title}");

        if (!response.IsSuccessStatusCode)
            return new List<MovieModel>();

        var json = await response.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var movies = JsonSerializer.Deserialize<List<MovieModel>>(json, options);

        return movies ?? new List<MovieModel>();
    }

    public static async Task<List<MovieModel>> GetFavoritesAsync()
    {
        var client = GetClientWithAuth();
        var response = await client.GetAsync("http://localhost:5271/api/Favorites");

        if (!response.IsSuccessStatusCode) return new List<MovieModel>();

        var json = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var favorites = JsonSerializer.Deserialize<List<FavoriteMovieModel>>(json, options)!;

        return favorites.Select(f => new MovieModel
        {
            ImdbID = f.ImdbID,
            Title = f.Title,
            Year = f.Year,
            Poster = f.Poster,
            IsFavorite = true
        }).ToList();
    }


    public static async Task<bool> AddToFavoritesAsync(MovieModel movie)
    {
        if (!AuthSession.IsAuthenticated) return false;

        var client = GetClientWithAuth();

        var request = new FavoriteMovieModel
        {
            ImdbID = movie.ImdbID,
            Title = movie.Title,
            Year = movie.Year,
            Poster = movie.Poster,
            Comment = null
        };

        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("http://localhost:5271/api/Favorites", content);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            Debug.WriteLine($"Error al agregar favorita: {(int)response.StatusCode} {response.ReasonPhrase}");
            Debug.WriteLine($"Mensaje del servidor: {error}");
        }

        return response.IsSuccessStatusCode;
    }




    public static async Task<bool> RemoveFromFavoritesAsync(string imdbId)
    {
        if (!AuthSession.IsAuthenticated) return false;

        var client = GetClientWithAuth();
        var response = await client.DeleteAsync($"http://localhost:5271/api/Favorites/{imdbId}");
        return response.IsSuccessStatusCode;
    }

}
