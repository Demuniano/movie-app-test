namespace MovieApp.API.DTOs
{
    public class FavoriteMovieRequest
    {
        public string ImdbID { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Year { get; set; } = string.Empty;
        public string Poster { get; set; } = string.Empty;
        public string? Comment { get; set; }
    }
}
