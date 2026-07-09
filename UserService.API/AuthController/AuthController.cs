using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.API.Models.RequestModels.Authentication;
using UserService.API.Models.ResponseModels.Authentication;
using UserService.Application.Features.Authentication.Register;
using UserService.Application.Features.Login;
using UserService.Application.Features.Logout;

namespace UserService.API.AuthController
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var command = new RegisterCommand(
                request.Name,
                request.Password);

                await _mediator.Send(command, cancellationToken);

                return Ok(new
                {
                    message = "User successfully registered"
                });

            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request, CancellationToken cancellationToken)
        {
            var command = new LoginCommand(
                request.Name,
                request.Password);

            var token = await _mediator.Send(command, cancellationToken);


            return Ok(new LoginResponse
            {
                Token = token
            });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout(CancellationToken cancellationToken)
        {
            await _mediator.Send(
                new LogoutCommand(), cancellationToken);

            return Ok(new
            {
                message = "Logged out successfully"
            });
        }
    }
}
