using Microsoft.EntityFrameworkCore;
using MovieApp.API.Models;

namespace MovieApp.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<FavoriteMovie> FavoriteMovies => Set<FavoriteMovie>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FavoriteMovie>()
                .HasIndex(f => new { f.UserId, f.ImdbID })
                .IsUnique();
        }

    }
}