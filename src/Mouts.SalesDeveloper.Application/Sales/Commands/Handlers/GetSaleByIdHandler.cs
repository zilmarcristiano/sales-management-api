using Mouts.SalesDeveloper.Application.Dtos;
using Mouts.SalesDeveloper.Application.Sales.Queries;
using Mouts.SalesDeveloper.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Mouts.SalesDeveloper.Application.Sales.Commands.Handlers;

public class GetSaleByIdHandler : IRequestHandler<GetSaleByIdQuery, SaleResponse?>
{
    private readonly ISaleRepository _repo;
    private readonly IMapper _mapper;
    private readonly ILogger<GetSaleByIdHandler> _logger;

    public GetSaleByIdHandler(ISaleRepository repo, IMapper mapper, ILogger<GetSaleByIdHandler> logger)
    {
        _repo = repo;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<SaleResponse?> Handle(GetSaleByIdQuery request, CancellationToken ct)
    {
        _logger.LogInformation("Start - Retrieving sale by id {SaleId}", request.Id);

        var sale = await _repo.Query()
            .Include(s => s.Items)
            .FirstOrDefaultAsync(s => s.Id == request.Id, ct);

        if (sale is null)
        {
            _logger.LogWarning("Sale {SaleId} not found", request.Id);
            return null;
        }

        _logger.LogInformation("Sale {SaleId} found", request.Id);
        return _mapper.Map<SaleResponse>(sale);
    }
}
