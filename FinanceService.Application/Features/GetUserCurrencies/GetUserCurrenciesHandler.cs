using FinanceService.Application.DTOs;
using FinanceService.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceService.Application.Features.GetUserCurrencies
{
    public class GetUserCurrenciesHandler : IRequestHandler<GetUserCurrenciesQuery, List<CurrencyDto>>
    {

        private readonly IFinanceRepository _repository;


        public GetUserCurrenciesHandler(
            IFinanceRepository repository)
        {
            _repository = repository;
        }


        public async Task<List<CurrencyDto>> Handle(
            GetUserCurrenciesQuery request,
            CancellationToken cancellationToken)
        {
            return await _repository
                .GetUserCurrenciesAsync(
                    request.UserId,
                    cancellationToken);
        }
    }
}
