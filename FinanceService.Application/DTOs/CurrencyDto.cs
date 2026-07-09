using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceService.Application.DTOs
{
    public class CurrencyDto
    {
        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public decimal Rate { get; set; }
    }
}
