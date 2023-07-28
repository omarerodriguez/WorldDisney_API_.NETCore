using Microsoft.EntityFrameworkCore;
using MundoDeDisney.MundoDeDisney.Core.Entities;
using System.Reflection;

namespace MundoDeDisney.MundoDeDisney.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        
        public DbSet<Genre> Genres => Set<Genre>();
        public DbSet<Character> Characters => Set<Character>();
        public DbSet<Movie> Movies => Set<Movie>();
        public DbSet<MovieCharacter> MoviesCharacters => Set<MovieCharacter>();
        public DbSet<User> Users => Set<User>();
    }
}
