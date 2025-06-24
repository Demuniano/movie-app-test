using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MovieApp.API.Data;
using MovieApp.API.Repositories.Implementations;
using MovieApp.API.Repositories.Interfaces;
using MovieApp.API.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Cargar variables desde .env
Env.Load();
builder.Configuration.AddEnvironmentVariables();

// Conexión desde variable de entorno
var connectionString = Environment.GetEnvironmentVariable("SQLITE_DB");
var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET");
Console.WriteLine($"🔐 JWT_SECRET: {jwtSecret}");
if (string.IsNullOrWhiteSpace(connectionString) || string.IsNullOrWhiteSpace(jwtSecret))
{
    throw new Exception("Faltan variables de entorno: SQLITE_DB o JWT_SECRET");
}

// Configurar DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

// Repositorios
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IFavoriteMovieRepository, FavoriteMovieRepository>();

// Servicios
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddHttpClient<OmdbService>();

// Autenticación JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret!))
        };
    });

// Swagger & Controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // <- Obligatorio antes de Authorization
app.UseAuthorization();

app.MapControllers();

app.Run();
