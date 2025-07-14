using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Mouts.SalesDeveloper.Domain.Entities;
using Mouts.SalesDeveloper.Infrastructure.Data;
using Mouts.SalesDeveloper.Infrastructure.Data.Repositories;
using Xunit;

namespace Mouts.SalesDeveloper.Tests.Repositories
{
    public class SaleRepositoryTests
    {
        private readonly DefaultContext _context;
        private readonly SaleRepository _repository;

        public SaleRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<DefaultContext>()
                .UseInMemoryDatabase("SalesRepositoryTestsDb")
                .Options;

            _context = new DefaultContext(options);
            _repository = new SaleRepository(_context);
        }

        [Fact]
        public async Task AddAsync_ShouldPersistSale()
        {
            var sale = BuildSale();

            await _repository.AddAsync(sale);
            await _repository.SaveChangesAsync();

            var result = await _context.Sales.Include(s => s.Items).FirstOrDefaultAsync(s => s.Id == sale.Id);

            result.Should().NotBeNull();
            result!.Items.Should().HaveCount(1);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnSaleWithItems()
        {
            var sale = BuildSale();
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();

            var result = await _repository.GetByIdAsync(sale.Id);

            result.Should().NotBeNull();
            result!.Items.Should().HaveCount(1);
        }

        [Fact]
        public async Task ExistsAsync_ShouldReturnTrue_WhenSaleExists()
        {
            var sale = BuildSale();
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();

            var exists = await _repository.ExistsAsync(sale.Id);

            exists.Should().BeTrue();
        }

        [Fact]
        public async Task RemoveAllItems_ShouldRemoveSaleItems()
        {
            var sale = BuildSale();
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();

            _repository.RemoveAllItems(sale);
            await _repository.SaveChangesAsync();

            var updatedSale = await _context.Sales.Include(s => s.Items).FirstOrDefaultAsync(s => s.Id == sale.Id);

            updatedSale!.Items.Should().BeEmpty();
        }

        private Sale BuildSale()
        {
            return new Sale
            {
                Id = Guid.NewGuid(),
                SaleNumber = "TEST123",
                SaleDate = DateTime.UtcNow,
                CustomerId = Guid.NewGuid(),
                CustomerName = "Test Client",
                BranchId = Guid.NewGuid(),
                Items = new List<SaleItem>
                {
                    new SaleItem
                    {
                        ProductId = Guid.NewGuid(),
                        ProductName = "Test Product",
                        Quantity = 1,
                        UnitPrice = 10
                    }
                }
            };
        }
    }
}
