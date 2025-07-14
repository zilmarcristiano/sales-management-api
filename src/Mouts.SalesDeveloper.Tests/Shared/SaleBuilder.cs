using Mouts.SalesDeveloper.Domain.Entities;

namespace Mouts.SalesDeveloper.Tests.Shared
{
    public class SaleBuilder
    {
        private readonly Sale _sale;

        public SaleBuilder()
        {
            _sale = new Sale
            {
                Id = Guid.NewGuid(),
                SaleNumber = "S-TEST",
                SaleDate = DateTime.UtcNow,
                CustomerId = Guid.NewGuid(),
                CustomerName = "Test Customer",
                BranchId = Guid.NewGuid(),
                Items = new List<SaleItem>()
            };
        }

        public SaleBuilder WithItem(SaleItem item)
        {
            _sale.Items.Add(item);
            return this;
        }

        public Sale Build() => _sale;
    }
}
