using Mouts.SalesDeveloper.Application.Dtos;
using Mouts.SalesDeveloper.Application.Resources;
using FluentValidation;

namespace Mouts.SalesDeveloper.Application.Sales.Validators
{
    public class UpdateSaleValidator : AbstractValidator<SaleRequest>
    {
        public UpdateSaleValidator()
        {
            RuleFor(x => x.SaleNumber)
                .NotEmpty().WithMessage(ValidationMessages.SaleNumberRequired);

            RuleFor(x => x.SaleDate)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage(ValidationMessages.SaleDateInFuture);

            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage(ValidationMessages.CustomerRequired);

            RuleFor(x => x.Items)
                .NotEmpty().WithMessage(ValidationMessages.SaleMustHaveItems);

            RuleForEach(x => x.Items).ChildRules(items =>
            {
                items.RuleFor(i => i.ProductId)
                    .NotEmpty().WithMessage(ValidationMessages.ProductRequired);

                items.RuleFor(i => i.Quantity)
                    .GreaterThan(0).WithMessage(ValidationMessages.QuantityGreaterThanZero)
                    .LessThanOrEqualTo(20).WithMessage(ValidationMessages.QuantityLimit);

                items.RuleFor(i => i.UnitPrice)
                    .GreaterThan(0).WithMessage(ValidationMessages.UnitPriceGreaterThanZero);
            });
        }
    }
}
