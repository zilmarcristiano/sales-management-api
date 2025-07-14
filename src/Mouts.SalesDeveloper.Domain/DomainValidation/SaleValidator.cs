using Mouts.SalesDeveloper.Domain.Entities;
using Mouts.SalesDeveloper.Domain.Exceptions;

namespace Mouts.SalesDeveloper.Domain.DomainValidation
{
    public class SaleValidator : IValidator<Sale>
    {
        public void Validate(Sale sale)
        {
            if (string.IsNullOrWhiteSpace(sale.CustomerName))
                throw new DomainException("The customer name is required.");

            if (!sale.Items.Any())
                throw new DomainException("The sale must contain at least one item.");

            if (sale.TotalAmount <= 0)
                throw new DomainException("The total sale amount must be greater than zero.");
        }
    }
}
