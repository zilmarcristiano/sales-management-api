using Mouts.SalesDeveloper.Application.Sales.Commands;
using Mouts.SalesDeveloper.Application.Resources;
using FluentValidation;

namespace Mouts.SalesDeveloper.Application.Sales.Validators
{
    public class CancelSaleValidator : AbstractValidator<CancelSaleCommand>
    {
        public CancelSaleValidator()
        {
            RuleFor(x => x.SaleId)
                .NotEmpty().WithMessage(ValidationMessages.SaleIdRequired);

            RuleFor(x => x.Reason)
                .NotEmpty().WithMessage(ValidationMessages.CancellationReasonRequired)
                .MinimumLength(5).WithMessage(ValidationMessages.CancellationReasonDetailed);
        }
    }
}
