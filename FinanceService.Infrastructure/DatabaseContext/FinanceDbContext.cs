using FinanceService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceService.Infrastructure.DatabaseContext
{
    public class FinanceDbContext : DbContext
    {
        public FinanceDbContext(
            DbContextOptions<FinanceDbContext> options)
            : base(options)
        {
        }


        public DbSet<Currency> Currencies { get; set; }

        public DbSet<FavoriteCurrency> FavoriteCurrencies { get; set; }


        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>()
                .ToTable("Currencies");


            modelBuilder.Entity<FavoriteCurrency>()
                .ToTable("FavoriteCurrencies");


            base.OnModelCreating(modelBuilder);
        }
    }
}
