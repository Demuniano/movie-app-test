using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using MovieApp.API.Models;
using MovieApp.API.Repositories.Interfaces;
using MovieApp.API.DTOs;

namespace MovieApp.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class FavoritesController : ControllerBase
    {
        private readonly IFavoriteMovieRepository _favoriteRepo;

        public FavoritesController(IFavoriteMovieRepository favoriteRepo)
        {
            _favoriteRepo = favoriteRepo;
        }

        [HttpPost]
        public async Task<IActionResult> AddFavorite([FromBody] FavoriteMovieRequest dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

            if (await _favoriteRepo.ExistsAsync(userId, dto.ImdbID))
                return Conflict("This movie is already in your favorites");

            var favorite = new FavoriteMovie
            {
                UserId = userId,
                ImdbID = dto.ImdbID,
                Title = dto.Title,
                Year = dto.Year,
                Poster = dto.Poster,
                Comment = dto.Comment,
                SavedAt = DateTime.UtcNow
            };

            await _favoriteRepo.AddFavoriteAsync(favorite);
            return Ok(favorite);
        }

        [HttpGet]
        public async Task<IActionResult> GetFavorites()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            var favorites = await _favoriteRepo.GetFavoritesByUserAsync(userId);
            return Ok(favorites);
        }

        [HttpDelete("{imdbId}")]
        public async Task<IActionResult> DeleteFavorite(string imdbId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            await _favoriteRepo.RemoveFavoriteAsync(userId, imdbId);
            return NoContent();
        }
    }
}
