using Microsoft.AspNetCore.Mvc;
using MovieApp.API.Services;

namespace MovieApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly OmdbService _omdbService;

        public MoviesController(OmdbService omdbService)
        {
            _omdbService = omdbService;
        }

        [HttpGet]
        public async Task<IActionResult> SearchMovies([FromQuery] string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                return BadRequest("Title is required");

            var results = await _omdbService.SearchMoviesByTitleAsync(title);

            if (results == null || !results.Any())
                return NotFound("No movies found");

            return Ok(results);
        }
    }
}
