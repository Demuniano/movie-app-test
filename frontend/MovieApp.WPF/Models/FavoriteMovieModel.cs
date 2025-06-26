namespace MovieApp.WPF.Models
{
    public class FavoriteMovieModel
    {
        public int Id { get; set; }
        public string ImdbID { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Year { get; set; } = string.Empty;
        public string Poster { get; set; } = string.Empty;
        public string? Comment { get; set; }
        public DateTime SavedAt { get; set; }
        public int UserId { get; set; }

        // Propiedad auxiliar para saber si está marcada como favorita en el frontend
        public bool IsFavorite { get; set; } = true;
    }
}
