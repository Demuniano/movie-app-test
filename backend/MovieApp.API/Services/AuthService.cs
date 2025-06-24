using MovieApp.API.Repositories.Interfaces;
using MovieApp.API.Services;
using MovieApp.API.Models;
using BCrypt.Net;

public class AuthService
{
    private readonly IUserRepository _userRepository;
    private readonly JwtService _jwtService;

    public AuthService(IUserRepository userRepository, JwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task<bool> RegisterAsync(string email, string password)
    {
        if (await _userRepository.ExistsByEmailAsync(email))
            return false;

        var user = new User
        {
            Email = email,
            Password = BCrypt.Net.BCrypt.HashPassword(password)
        };

        await _userRepository.AddAsync(user);
        return true;
    }

    public async Task<string?> LoginAsync(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            return null;

        return _jwtService.GenerateToken(user.Id, user.Email);
    }
}
