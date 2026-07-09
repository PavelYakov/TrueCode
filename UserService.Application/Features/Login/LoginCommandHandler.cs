using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.Interfaces.Repositories;
using UserService.Application.Interfaces.Services;

namespace UserService.Application.Features.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;


        public LoginCommandHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IJwtProvider jwtProvider)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
        }


        public async Task<string> Handle(
            LoginCommand request,
            CancellationToken cancellationToken)
        {
            // 1. Ищем пользователя
            var user = await _userRepository
                .GetByNameAsync(request.Name);


            // 2. Если пользователя нет
            if (user == null)
            {
                throw new Exception("Invalid username or password");
            }


            // 3. Проверяем пароль
            var isPasswordValid = _passwordHasher
                .VerifyPassword(
                    request.Password,
                    user.Password);


            if (!isPasswordValid)
            {
                throw new Exception("Invalid username or password");
            }


            // 4. Генерируем JWT
            var token = _jwtProvider
                .GenerateToken(user);


            // 5. Возвращаем токен
            return token;
        }
    }
}
