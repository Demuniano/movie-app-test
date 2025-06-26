using Microsoft.EntityFrameworkCore;
using MovieApp.API.Data;
using MovieApp.API.Models;
using MovieApp.API.Repositories.Interfaces;

namespace MovieApp.API.Repositories.Implementations
{
    public class FavoriteMovieRepository : IFavoriteMovieRepository
    {
        private readonly ApplicationDbContext _context;

        public FavoriteMovieRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddFavoriteAsync(FavoriteMovie favorite)
        {
            _context.FavoriteMovies.Add(favorite);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int userId, string imdbId)
        {
            return await _context.FavoriteMovies
                .AnyAsync(f => f.UserId == userId && f.ImdbID == imdbId);
        }

        public async Task<List<FavoriteMovie>> GetFavoritesByUserAsync(int userId)
        {
            return await _context.FavoriteMovies
                .Where(f => f.UserId == userId)
                .OrderByDescending(f => f.SavedAt)
                .ToListAsync();
        }

        public async Task RemoveFavoriteAsync(int userId, string imdbId)
        {
            var favorite = await _context.FavoriteMovies
                .FirstOrDefaultAsync(f => f.UserId == userId && f.ImdbID == imdbId);

            if (favorite != null)
            {
                _context.FavoriteMovies.Remove(favorite);
                await _context.SaveChangesAsync();
            }
        }
    }
}
