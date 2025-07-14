using Mouts.SalesDeveloper.Application.Sales.Commands;
using FluentValidation;

namespace Mouts.SalesDeveloper.Application.Sales.Validators
{
    public class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
    {
        public CreateSaleValidator()
        {
            RuleFor(x => x.Request.CustomerName).NotEmpty();
            RuleForEach(x => x.Request.Items).ChildRules(i =>
            {
                i.RuleFor(x => x.Quantity).GreaterThan(0).LessThanOrEqualTo(20);
            });
        }
    }
}