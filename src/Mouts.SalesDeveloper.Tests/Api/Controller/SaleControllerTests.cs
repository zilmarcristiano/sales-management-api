using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Mouts.SalesDeveloper.Api.Controller;
using Mouts.SalesDeveloper.Application.Dtos;
using Mouts.SalesDeveloper.Application.Sales.Commands;
using Xunit;

namespace Mouts.SalesDeveloper.Tests.Api.Controller
{
    public class SaleControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock = new();
        private readonly SaleController _controller;

        public SaleControllerTests()
        {
            _controller = new SaleController(_mediatorMock.Object);
        }

        [Fact]
        public async Task Create_ShouldReturnCreatedAt_WhenValidRequest()
        {
            var request = new SaleRequest { SaleNumber = "S001", CustomerName = "Test", Items = new List<SaleItemRequest> { new() { ProductId = Guid.NewGuid(), ProductName = "Item", Quantity = 5, UnitPrice = 10 } } };

            _mediatorMock.Setup(x => x.Send(It.IsAny<CreateSaleCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new SaleResponse { Id = Guid.NewGuid(), SaleNumber = "S001" });

            var result = await _controller.Create(request);

            result.Should().BeOfType<CreatedAtActionResult>();
        }
    }
}