using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceService.Domain.Entities
{
    public class FavoriteCurrency
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int CurrencyId { get; set; }

        public Currency Currency { get; set; } = null!;
    }
}
