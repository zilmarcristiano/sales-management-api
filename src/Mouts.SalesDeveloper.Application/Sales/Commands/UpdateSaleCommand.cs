using Mouts.SalesDeveloper.Application.Dtos;
using MediatR;

namespace Mouts.SalesDeveloper.Application.Sales.Commands
{
    public record UpdateSaleCommand(Guid Id, SaleRequest Request) : IRequest<SaleResponse>;
}
