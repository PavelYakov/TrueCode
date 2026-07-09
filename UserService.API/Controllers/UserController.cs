using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserService.API.Models.RequestModels;
using UserService.Application.Features.FavoriteCurrencies.AddFavoriteCurrency;

namespace UserService.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("favorites")]
        public async Task<IActionResult> AddFavorite(AddFavoriteRequest request)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            await _mediator.Send(new AddFavoriteCurrencyCommand(userId, request.CurrencyId));

            return Ok(new
            {
                Message = "Successfully"
            });
        }
    }
}
