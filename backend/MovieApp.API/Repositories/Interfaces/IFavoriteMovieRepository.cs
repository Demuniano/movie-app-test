using MovieApp.API.Models;

namespace MovieApp.API.Repositories.Interfaces
{
    public interface IFavoriteMovieRepository
    {
        Task AddFavoriteAsync(FavoriteMovie favorite);
        Task<bool> ExistsAsync(int userId, string imdbId);
        Task<List<FavoriteMovie>> GetFavoritesByUserAsync(int userId);
        Task RemoveFavoriteAsync(int userId, string imdbId);
    }
}
