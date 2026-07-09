using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain.Entites;

namespace UserService.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByNameAsync(string name);

        Task AddAsync(User user);

        Task<bool> ExistsAsync(string name);

        Task AddFavoriteCurrencyAsync(int userId, int currencyId);

        Task<bool> FavoriteExistsAsync(int userId, int currencyId);
    }
}
