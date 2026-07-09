using FinanceService.Application.DTOs;
using FinanceService.Application.Interfaces.Repositories;
using FinanceService.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceService.Infrastructure.Repositories
{
    public class FinanceRepository : IFinanceRepository
    {
        private readonly FinanceDbContext _context;


        public FinanceRepository(
            FinanceDbContext context)
        {
            _context = context;
        }


        public async Task<List<CurrencyDto>> GetUserCurrenciesAsync(
            int userId,
            CancellationToken cancellationToken)
        {
            return await _context.FavoriteCurrencies
                .Where(x => x.UserId == userId)
                .Select(x => new CurrencyDto
                {
                    Code = x.Currency.Code,

                    Name = x.Currency.Name,

                    Rate = x.Currency.Rate
                })
                .ToListAsync(cancellationToken);
        }
    }
}
