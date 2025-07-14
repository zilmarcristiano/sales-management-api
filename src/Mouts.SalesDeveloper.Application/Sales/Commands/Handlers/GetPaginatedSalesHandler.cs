using Mouts.SalesDeveloper.Application.Dtos;
using Mouts.SalesDeveloper.Application.Sales.Queries;
using Mouts.SalesDeveloper.Domain.Entities;
using Mouts.SalesDeveloper.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Mouts.SalesDeveloper.Application.Sales.Commands.Handlers
{
    public class GetPaginatedSalesHandler : IRequestHandler<GetPaginatedSalesQuery, PaginatedResult<SaleResponse>>
    {
        private readonly ISaleRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPaginatedSalesHandler> _logger;

        public GetPaginatedSalesHandler(ISaleRepository repo, IMapper mapper, ILogger<GetPaginatedSalesHandler> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PaginatedResult<SaleResponse>> Handle(GetPaginatedSalesQuery request, CancellationToken ct)
        {
            _logger.LogInformation("Start - Retrieving paginated sales - Page: {Page}, Size: {Size}", request.Page, request.Size);

            var query = (await _repo.GetQueryableAsync()).Include(s => s.Items);
            var paginated = await PaginatedList<Sale>.CreateAsync(query, request.Page, request.Size);

            _logger.LogInformation("Retrieved {Count} sales from page {Page}", paginated.Count, request.Page);

            var mappedItems = _mapper.Map<List<SaleResponse>>(paginated.ToList());

            return PaginatedResult<SaleResponse>.Ok(
                mappedItems,
                paginated.CurrentPage,
                paginated.TotalPages,
                paginated.TotalCount
            );
        }
    }
}
