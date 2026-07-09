using FinanceService.Application.Features.GetUserCurrencies;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinanceService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FinanceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FinanceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("currencies")]
        public async Task<IActionResult> GetCurrencies()
        {
            var userId = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var result = await _mediator.Send(
                new GetUserCurrenciesQuery(userId));

            return Ok(result);
        }
    }
}
