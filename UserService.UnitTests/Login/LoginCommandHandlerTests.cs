using FluentAssertions;
using Moq;
using UserService.Application.Features.Login;
using UserService.Application.Interfaces.Repositories;
using UserService.Application.Interfaces.Services;
using UserService.Domain.Entites;

namespace UserService.UnitTests.Login
{
    public class LoginCommandHandlerTests
    {
        private readonly Mock<IUserRepository> _repositoryMock;
        private readonly Mock<IPasswordHasher> _passwordHasherMock;
        private readonly Mock<IJwtProvider> _jwtProviderMock;

        private readonly LoginCommandHandler _handler;


        public LoginCommandHandlerTests()
        {
            _repositoryMock = new Mock<IUserRepository>();

            _passwordHasherMock = new Mock<IPasswordHasher>();

            _jwtProviderMock = new Mock<IJwtProvider>();

            _handler = new LoginCommandHandler(
                _repositoryMock.Object,
                _passwordHasherMock.Object,
                _jwtProviderMock.Object);
        }

        [Fact]
        public async Task Login_Fail_When_User_Not_Found()
        {
            // Arrange

            _repositoryMock
                .Setup(x => x.GetByNameAsync("UserName1"))
                .ReturnsAsync((User?)null);


            var command = new LoginCommand(
                "UserName1",
                "123456");


            // Act

            Func<Task> action = async () =>
            {
                await _handler.Handle(
                    command,
                    CancellationToken.None);
            };


            // Assert

            await action.Should()
                .ThrowAsync<Exception>();
        }

        [Fact]
        public async Task Login_Fail_When_Password_Is_Invalid()
        {
            // Arrange

            var user = new User
            {
                Id = 1,
                Name = "UserName1",
                Password = "hashed_password"
            };


            _repositoryMock
                .Setup(x => x.GetByNameAsync("UserName1"))
                .ReturnsAsync(user);


            _passwordHasherMock
                .Setup(x => x.VerifyPassword(
                    "123456",
                    "hashed_password"))
                .Returns(false);


            var command = new LoginCommand(
                "UserName1",
                "123456");


            // Act

            Func<Task> action = async () =>
            {
                await _handler.Handle(
                    command,
                    CancellationToken.None);
            };


            // Assert

            await action.Should()
                .ThrowAsync<Exception>();


            _jwtProviderMock.Verify(
                x => x.GenerateToken(It.IsAny<User>()),
                Times.Never);
        }

        [Fact]
        public async Task Login_Return_Token_When_Credentials_Are_Valid()
        {
            // Arrange

            var user = new User
            {
                Id = 1,
                Name = "UserName1",
                Password = "hashed_password"
            };


            _repositoryMock
                .Setup(x => x.GetByNameAsync("UserName1"))
                .ReturnsAsync(user);


            _passwordHasherMock
                .Setup(x => x.VerifyPassword(
                    "123456",
                    "hashed_password"))
                .Returns(true);


            _jwtProviderMock
                .Setup(x => x.GenerateToken(user))
                .Returns("jwt_token");


            var command = new LoginCommand(
                "UserName1",
                "123456");


            // Act

            var result = await _handler.Handle(
                command,
                CancellationToken.None);


            // Assert

            result.Should()
                .Be("jwt_token");


            _jwtProviderMock.Verify(
                x => x.GenerateToken(user),
                Times.Once);
        }
    }
}
