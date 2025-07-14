using Mouts.SalesDeveloper.Application.Dtos;
using MediatR;

namespace Mouts.SalesDeveloper.Application.Sales.Queries
{
    public record GetAllSalesQuery() : IRequest<IEnumerable<SaleResponse>>;
}
