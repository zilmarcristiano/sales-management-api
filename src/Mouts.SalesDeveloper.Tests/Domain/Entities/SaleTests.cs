using FluentAssertions;
using Mouts.SalesDeveloper.Domain.Entities;
using Xunit;

namespace Mouts.SalesDeveloper.Tests.Domain.Entities
{
    public class SaleTests
    {
        [Fact]
        public void CalculateDiscounts_ShouldApply10Percent_WhenQuantityIs5()
        {
            var sale = new Sale
            {
                Items = new List<SaleItem>
        {
            new SaleItem { ProductId = Guid.NewGuid(), Quantity = 5, UnitPrice = 100 } // Valor unitário alto para exemplo
        }
            };

            sale.CalculateDiscounts();

            sale.Items.First().Discount.Should().Be(50); // 100 * 0.10 * 5 = 50
        }


        [Fact]
        public void CalculateDiscounts_ShouldApply20Percent_WhenQuantityIs15()
        {
            var sale = new Sale
            {
                Items = new List<SaleItem>
        {
            new SaleItem { ProductId = Guid.NewGuid(), Quantity = 15, UnitPrice = 200 }
        }
            };

            sale.CalculateDiscounts();

            sale.Items.First().Discount.Should().Be(200 * 0.20m * 15); // Esperado: 600.00m
        }


        [Fact]
        public void CalculateDiscounts_ShouldNotApplyDiscount_WhenQuantityIsLessThan4()
        {
            var sale = new Sale
            {
                Items = new List<SaleItem>
            {
                new() { Quantity = 2, UnitPrice = 50 }
            }
            };

            sale.CalculateDiscounts();

            sale.Items.First().Discount.Should().Be(0);
        }
    }
}