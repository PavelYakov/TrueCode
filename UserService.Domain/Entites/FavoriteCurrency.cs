using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Domain.Entites
{
    // Связующая таблица User и Currency
    public class FavoriteCurrency
    {
        public int UserId { get; set; }

        public int CurrencyId { get; set; }

        public User User { get; set; } = null!;
    }
}
