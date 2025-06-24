using Microsoft.AspNetCore.Mvc;
using MovieApp.API.Services;
using MovieApp.API.DTOs;

namespace MovieApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest dto)
        {
            var success = await _authService.RegisterAsync(dto.Email, dto.Password);
            if (!success)
                return BadRequest("Email already in use");

            return Ok(new { message = "User registered successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest dto)
        {
            var token = await _authService.LoginAsync(dto.Email, dto.Password);
            if (token == null)
                return Unauthorized("Invalid credentials");

            return Ok(new { token });
        }
    }
}
