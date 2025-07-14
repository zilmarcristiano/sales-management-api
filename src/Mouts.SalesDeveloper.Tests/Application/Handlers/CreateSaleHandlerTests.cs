using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Mouts.SalesDeveloper.Application.Dtos;
using Mouts.SalesDeveloper.Application.Sales.Commands.Handlers;
using Mouts.SalesDeveloper.Application.Sales.Commands;
using Mouts.SalesDeveloper.Domain.Entities;
using Mouts.SalesDeveloper.Domain.Exceptions;
using Mouts.SalesDeveloper.Domain.Repositories;
using Xunit;

namespace Mouts.SalesDeveloper.Tests.Application.Handlers
{
    public class CreateSaleHandlerTests
    {
        private readonly Mock<ISaleRepository> _repoMock = new();
        private readonly Mock<IUnitOfWork> _uowMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly Mock<ILogger<CreateSaleHandler>> _loggerMock = new();

        private readonly CreateSaleHandler _handler;

        public CreateSaleHandlerTests()
        {
            _handler = new CreateSaleHandler(_repoMock.Object, _uowMock.Object, _mapperMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenSaleNumberIsEmpty()
        {
            var request = new CreateSaleCommand(new SaleRequest { SaleNumber = "" });

            Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

            await act.Should().ThrowAsync<DomainException>().WithMessage("Sale number is required.");
        }

        [Fact]
        public async Task Handle_ShouldCallRepository_WhenValidRequest()
        {
            var saleRequest = new SaleRequest
            {
                SaleNumber = "S001",
                Items = new List<SaleItemRequest> { new() { ProductId = Guid.NewGuid(), ProductName = "Test", Quantity = 5, UnitPrice = 10 } }
            };

            var saleEntity = new Sale();

            _mapperMock.Setup(x => x.Map<Sale>(saleRequest)).Returns(saleEntity);
            _mapperMock.Setup(x => x.Map<SaleResponse>(saleEntity)).Returns(new SaleResponse { SaleNumber = "S001" });

            var result = await _handler.Handle(new CreateSaleCommand(saleRequest), CancellationToken.None);

            _repoMock.Verify(x => x.AddAsync(It.IsAny<Sale>()), Times.Once);
            _uowMock.Verify(x => x.CommitAsync(), Times.Once);
            result.SaleNumber.Should().Be("S001");
        }
    }
}
