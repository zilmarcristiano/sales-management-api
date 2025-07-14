
using Mouts.SalesDeveloper.Application.Dtos;
using MediatR;

namespace Mouts.SalesDeveloper.Application.Sales.Commands
{
    public record CreateSaleCommand(SaleRequest Request) : IRequest<SaleResponse>;
}
