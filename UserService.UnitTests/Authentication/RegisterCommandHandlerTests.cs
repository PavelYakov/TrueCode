using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.Features.Authentication.Register;
using UserService.Application.Interfaces.Repositories;
using UserService.Application.Interfaces.Services;
using UserService.Domain.Entites;

namespace UserService.UnitTests.Authentication
{
    public class RegisterCommandHandlerTests
    {
        private readonly Mock<IUserRepository> _repositoryMock;
        private readonly Mock<IPasswordHasher> _passwordHasherMock;

        private readonly RegisterCommandHandler _handler;

        public RegisterCommandHandlerTests()
        {
            _repositoryMock = new Mock<IUserRepository>();

            _passwordHasherMock = new Mock<IPasswordHasher>();

            _handler = new RegisterCommandHandler(_repositoryMock.Object, _passwordHasherMock.Object);
        }

        [Fact]
        public async Task Register_Should_Fail_When_User_Already_Exists()
        {
            // Arrange

            _repositoryMock
                .Setup(x => x.ExistsAsync("UserName1"))
                .ReturnsAsync(true);


            var command = new RegisterCommand(
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

            var exception = await action.Should()
                .ThrowAsync<Exception>();

            _repositoryMock.Verify(
                x => x.AddAsync(It.IsAny<User>()),
                Times.Never);
        }

        [Fact]
        public async Task Register_Should_Create_User_When_Data_Is_Valid()
        {
            // Arrange

            _repositoryMock
                .Setup(x => x.ExistsAsync("UserName1"))
                .ReturnsAsync(false);


            _passwordHasherMock
                .Setup(x => x.HashPassword("123456"))
                .Returns("hashed_password");


            var command = new RegisterCommand(
                "UserName1",
                "123456");


            User? createdUser = null;


            _repositoryMock
                .Setup(x => x.AddAsync(It.IsAny<User>()))
                .Callback<User>(user =>
                {
                    createdUser = user;
                })
                .Returns(Task.CompletedTask);


            // Act

            await _handler.Handle(
                command,
                CancellationToken.None);


            // Assert

            _repositoryMock.Verify(
                x => x.AddAsync(It.IsAny<User>()),
                Times.Once);


            createdUser.Should()
                .NotBeNull();


            createdUser!.Name
                .Should()
                .Be("UserName1");


            createdUser.Password
                .Should()
                .Be("hashed_password");
        }
    }
}
