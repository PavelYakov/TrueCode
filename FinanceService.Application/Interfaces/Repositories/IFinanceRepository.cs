using FinanceService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceService.Application.Interfaces.Repositories
{
    public interface IFinanceRepository
    {
        Task<List<CurrencyDto>> GetUserCurrenciesAsync(
        int userId,
        CancellationToken cancellationToken);
    }
}
