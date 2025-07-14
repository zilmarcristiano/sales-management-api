using Mouts.SalesDeveloper.Domain.Entities;
using Mouts.SalesDeveloper.Domain.Exceptions;

namespace Mouts.SalesDeveloper.Domain.DomainValidation
{
    public class SaleItemValidator : IValidator<SaleItem>
    {
        public void Validate(SaleItem item)
        {
            if (item.Quantity <= 0)
                throw new DomainException("The item quantity must be greater than zero.");

            if (item.UnitPrice <= 0)
                throw new DomainException("The unit price must be greater than zero.");
        }
    }
}
