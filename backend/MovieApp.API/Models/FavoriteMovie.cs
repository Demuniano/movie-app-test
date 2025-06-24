namespace MovieApp.API.Models;

public class FavoriteMovie
{
    public int Id { get; set; }
    public string ImdbID { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Year { get; set; } = null!;
    public string Poster { get; set; } = null!;
    public string? Comment { get; set; }
    public DateTime SavedAt { get; set; } = DateTime.UtcNow;

    public int UserId { get; set; }
    public User? User { get; set; }
}
