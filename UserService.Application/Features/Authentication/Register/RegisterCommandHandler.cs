using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.Interfaces.Repositories;
using UserService.Application.Interfaces.Services;
using UserService.Domain.Entites;

namespace UserService.Application.Features.Authentication.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand>
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ILogger<RegisterCommandHandler> _logger;

        public RegisterCommandHandler(
            IUserRepository repository,
            IPasswordHasher passwordHasher,
            ILogger<RegisterCommandHandler> logger)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
            _logger = logger;
        }

        public async Task Handle(
            RegisterCommand request,
            CancellationToken cancellationToken)
        {
            if (await _repository.ExistsAsync(request.Name))
            {
                _logger.LogWarning($"Registration failed. User {request.Name} already exists");

                throw new InvalidOperationException($"Registration failed. User {request.Name} already exists");
            }
                


            _logger.LogInformation($"Registering new user {request.Name}");

            var user = new User
            {
                Name = request.Name,
                Password = _passwordHasher.HashPassword(request.Password)
            };

            await _repository.AddAsync(user);

            _logger.LogInformation($"User {request.Name} successfully registered");
        }
    }
}
