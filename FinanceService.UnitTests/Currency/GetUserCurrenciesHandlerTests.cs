using FinanceService.Application.DTOs;
using FinanceService.Application.Features.GetUserCurrencies;
using FinanceService.Application.Interfaces.Repositories;
using FluentAssertions;
using Moq;

namespace FinanceService.UnitTests.Currency
{
    public class GetUserCurrenciesHandlerTests
    {
        private readonly Mock<IFinanceRepository> _repositoryMock;

        private readonly GetUserCurrenciesHandler _handler;

        public GetUserCurrenciesHandlerTests()
        {
            _repositoryMock =
                new Mock<IFinanceRepository>();


            _handler =
                new GetUserCurrenciesHandler(
                    _repositoryMock.Object);
        }

        [Fact]
        public async Task GetCurrencies_Should_Return_User_Currencies()
        {
            // Arrange
            var currencies = new List<CurrencyDto>
            {
                new()
                {
                    Code = "USD",
                    Name = "Доллар",
                    Rate = 80
                },

                new()
                {
                    Code = "EUR",
                    Name = "Евро",
                    Rate = 90
                }
            };


            _repositoryMock
                .Setup(x =>
                    x.GetUserCurrenciesAsync(1, CancellationToken.None))
                .ReturnsAsync(currencies);

            var query =
                new GetUserCurrenciesQuery(1);


            // Act
            var result =
                await _handler.Handle(
                    query,
                    CancellationToken.None);



            // Assert
            result.Should()
                .HaveCount(2);


            result.Should()
                .Contain(x => x.Code == "USD");


            result.Should()
                .Contain(x => x.Code == "EUR");
        }

        [Fact]
        public async Task GetCurrencies_Should_Return_Empty_List_When_User_Has_No_Currencies()
        {
            // Arrange
            _repositoryMock
                .Setup(x =>
                   x.GetUserCurrenciesAsync(1, CancellationToken.None))
                .ReturnsAsync(new List<CurrencyDto>());


            var query =
                new GetUserCurrenciesQuery(1);



            // Act
            var result =
                await _handler.Handle(
                    query,
                    CancellationToken.None);



            // Assert
            result.Should()
                .BeEmpty();
        }
    }
}
