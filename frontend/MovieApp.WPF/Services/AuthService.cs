using System.Net.Http;
using System.Text;
using System.Text.Json;
using MovieApp.WPF.Models;
using MovieApp.WPF.Helpers;

namespace MovieApp.WPF.Services;
public static class AuthService
{
    private static readonly HttpClient client = new();

    public static async Task<LoginResult> LoginAsync(string email, string password)
    {
        var content = new StringContent(JsonSerializer.Serialize(new { email, password }), Encoding.UTF8, "application/json");

        try
        {
            var response = await client.PostAsync("http://localhost:5271/api/Auth/login", content);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var loginResponse = JsonSerializer.Deserialize<LoginResponse>(responseBody, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                if (!string.IsNullOrEmpty(loginResponse?.Token))
                {
                    AuthSession.SetSession(loginResponse.Token, email);
                }

                return new LoginResult
                {
                    Success = true,
                    Token = loginResponse?.Token
                };
            }

            else
            {
                return new LoginResult
                {
                    Success = false,
                };
            }
        }
        catch (Exception ex)
        {
            return new LoginResult
            {
                Success = false,
                ErrorMessage = "Error al conectar con el servidor o procesar la solicitud: " + ex.Message
            };
        }
    }





    public static async Task<RegisterResult> RegisterAsync(string email, string password)
    {
        try
        {
            var content = new StringContent(
                JsonSerializer.Serialize(new { email, password }),
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync("http://localhost:5271/api/Auth/register", content);

            if (response.IsSuccessStatusCode)
            {
                return new RegisterResult { Success = true };
            }

            string error = await response.Content.ReadAsStringAsync();
            return new RegisterResult
            {
                Success = false,
                ErrorMessage = $"Error del servidor: {response.StatusCode} - {error}"
            };
        }
        catch (Exception ex)
        {
            return new RegisterResult
            {
                Success = false,
                ErrorMessage = $"Excepción: {ex.Message}"
            };
        }
    }


}
