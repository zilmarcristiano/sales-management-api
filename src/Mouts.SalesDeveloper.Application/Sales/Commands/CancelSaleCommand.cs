using MediatR;

namespace Mouts.SalesDeveloper.Application.Sales.Commands
{
    public record CancelSaleCommand(Guid SaleId, string Reason) : IRequest<Unit>;
}
