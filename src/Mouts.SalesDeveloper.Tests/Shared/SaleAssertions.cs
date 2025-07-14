using FluentAssertions;
using Mouts.SalesDeveloper.Domain.Entities;

namespace Mouts.SalesDeveloper.Tests.Shared
{
    public static class SaleAssertions
    {
        public static void ShouldHaveValidDiscount(this Sale sale)
        {
            sale.Items.Should().NotBeNullOrEmpty();
            foreach (var item in sale.Items)
            {
                if (item.Quantity >= 4 && item.Quantity <= 20)
                    item.Discount.Should().BeGreaterThan(0);
                else
                    item.Discount.Should().Be(0);
            }
        }
    }
}
