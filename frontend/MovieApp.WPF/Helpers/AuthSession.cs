namespace MovieApp.WPF.Helpers;

public static class AuthSession
{
    public static string? Token { get; private set; }
    public static string? Email { get; private set; }
    public static bool IsAuthenticated => !string.IsNullOrWhiteSpace(Token);
    public static bool IsLoggedIn => !string.IsNullOrEmpty(Token);

    public static void SetSession(string token, string email)
    {
        Token = token;
        Email = email;
    }

    public static void Clear()
    {
        Token = null;
        Email = null;
    }
}
