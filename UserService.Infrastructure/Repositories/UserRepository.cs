using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.Interfaces.Repositories;
using UserService.Domain.Entites;
using UserService.Infrastructure.DatabaseContext;

namespace UserService.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByNameAsync(string name)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<bool> ExistsAsync(string name)
        {
            return await _context.Users
                .AnyAsync(x => x.Name == name);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task AddFavoriteCurrencyAsync(int userId, int currencyId)
        {
            var favorite = new FavoriteCurrency
            {
                UserId = userId,
                CurrencyId = currencyId
            };

            _context.FavoriteCurrencies.Add(favorite);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> FavoriteExistsAsync(int userId, int currencyId)
        {
            return await _context.FavoriteCurrencies.AnyAsync(x =>
                x.UserId == userId &&
                x.CurrencyId == currencyId);
        }
    }
}
