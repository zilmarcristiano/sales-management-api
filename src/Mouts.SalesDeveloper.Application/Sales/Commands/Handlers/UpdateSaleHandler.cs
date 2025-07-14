using Mouts.SalesDeveloper.Application.Dtos;
using Mouts.SalesDeveloper.Application.Sales.Commands;
using Mouts.SalesDeveloper.Domain.Entities;
using Mouts.SalesDeveloper.Domain.Enums;
using Mouts.SalesDeveloper.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Mouts.SalesDeveloper.Application.Sales.Commands.Handlers;

public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, SaleResponse>
{
    private readonly ISaleRepository _repo;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateSaleHandler> _logger;

    public UpdateSaleHandler(ISaleRepository repo, IMapper mapper, ILogger<UpdateSaleHandler> logger)
    {
        _repo = repo;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<SaleResponse> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Start - Updating sale {SaleId}", request.Id);

        var existing = await _repo.GetByIdAsync(request.Id);
        if (existing is null)
        {
            _logger.LogWarning("Sale {SaleId} not found", request.Id);
            throw new Exception("Venda não encontrada.");
        }

        var updatedItems = _mapper.Map<List<SaleItem>>(request.Request.Items);

        if (!Enum.TryParse<SaleStatus>(request.Request.Status, true, out var parsedStatus))
        {
            _logger.LogWarning("Invalid sale status '{Status}' for sale {SaleId}", request.Request.Status, request.Id);
            throw new Exception("Status inválido.");
        }

        _repo.RemoveAllItems(existing);
        _logger.LogDebug("Removed all items from sale {SaleId}", request.Id);

        existing.Modify(request.Request.SaleNumber, updatedItems, parsedStatus);
        _logger.LogDebug("Modified sale {SaleId} with new data", request.Id);

        await _repo.UpdateAsync(existing);
        await _repo.SaveChangesAsync();

        _logger.LogInformation("Sale {SaleId} updated successfully", request.Id);

        return _mapper.Map<SaleResponse>(existing);
    }
}
