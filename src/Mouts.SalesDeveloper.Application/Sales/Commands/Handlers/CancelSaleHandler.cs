﻿using MediatR;
using Microsoft.Extensions.Logging;
using Mouts.SalesDeveloper.Domain.Repositories;

namespace Mouts.SalesDeveloper.Application.Sales.Commands.Handlers
{
    public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, Unit>
    {
        private readonly ISaleRepository _repo;
        private readonly ILogger<CancelSaleHandler> _logger;

        public CancelSaleHandler(ISaleRepository repo, ILogger<CancelSaleHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<Unit> Handle(CancelSaleCommand request, CancellationToken ct)
        {
            _logger.LogInformation("Start - Cancelling sale {SaleId} with reason: {Reason}", request.SaleId, request.Reason);

            var sale = await _repo.GetByIdAsync(request.SaleId);
            if (sale is null)
            {
                _logger.LogWarning("Sale {SaleId} not found", request.SaleId);
                throw new Exception("Sale not found.");
            }

            sale.Cancel(request.Reason);
            _logger.LogDebug("Sale {SaleId} marked as cancelled", request.SaleId);

            await _repo.UpdateAsync(sale);
            await _repo.SaveChangesAsync();

            _logger.LogInformation("Sale {SaleId} cancelled successfully", request.SaleId);

            return Unit.Value;
        }
    }
}
