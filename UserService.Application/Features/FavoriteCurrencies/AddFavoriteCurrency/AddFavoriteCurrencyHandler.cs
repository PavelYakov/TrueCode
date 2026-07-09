using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.Interfaces.Repositories;

namespace UserService.Application.Features.FavoriteCurrencies.AddFavoriteCurrency
{
    public class AddFavoriteCurrencyHandler : IRequestHandler<AddFavoriteCurrencyCommand>
    {
        private readonly IUserRepository _repository;

        public AddFavoriteCurrencyHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(AddFavoriteCurrencyCommand request, CancellationToken cancellationToken)
        {
            if (await _repository.FavoriteExistsAsync(request.UserId, request.CurrencyId))
            {
                return;
            }

            await _repository.AddFavoriteCurrencyAsync(request.UserId, request.CurrencyId);
        }
    }
}
