using Mouts.SalesDeveloper.Application.Dtos;
using MediatR;

namespace Mouts.SalesDeveloper.Application.Sales.Queries
{
    public record GetPaginatedSalesQuery(int Page, int Size) : IRequest<PaginatedResult<SaleResponse>>;
}
