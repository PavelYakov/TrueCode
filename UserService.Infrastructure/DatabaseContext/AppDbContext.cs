using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entites;

namespace UserService.Infrastructure.DatabaseContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();

        public DbSet<Currency> Currencies => Set<Currency>();
        public DbSet<FavoriteCurrency> FavoriteCurrencies => Set<FavoriteCurrency>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FavoriteCurrency>()
                .HasKey(x => new { x.UserId, x.CurrencyId });

            modelBuilder.Entity<FavoriteCurrency>()
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId);
        }
    }
}
