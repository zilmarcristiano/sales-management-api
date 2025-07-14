using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Mouts.SalesDeveloper.Application.Dtos;
using Mouts.SalesDeveloper.Domain.Entities;
using Mouts.SalesDeveloper.Domain.Exceptions;
using Mouts.SalesDeveloper.Domain.Repositories;

namespace Mouts.SalesDeveloper.Application.Sales.Commands.Handlers
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, SaleResponse>
    {
        private readonly ISaleRepository _repo;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateSaleHandler> _logger;

        public CreateSaleHandler(ISaleRepository repo, IUnitOfWork uow, IMapper mapper, ILogger<CreateSaleHandler> logger)
        {
            _repo = repo;
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<SaleResponse> Handle(CreateSaleCommand request, CancellationToken ct)
        {
            _logger.LogInformation("Start - Creating sale for customer {CustomerId}", request.Request.CustomerId);

            if (string.IsNullOrWhiteSpace(request.Request.SaleNumber))
                throw new DomainException("Sale number is required.");

            if (!request.Request.Items.Any())
                throw new DomainException("The sale must contain at least one item.");

            var sale = _mapper.Map<Sale>(request.Request);
            _logger.LogDebug("Mapped sale: {@Sale}", sale);

            sale.CalculateDiscounts();
            _logger.LogDebug("Discounts calculated for sale {SaleNumber}", sale.SaleNumber);

            await _repo.AddAsync(sale);
            await _uow.CommitAsync();

            _logger.LogInformation("Sale {SaleNumber} persisted successfully", sale.SaleNumber);

            return _mapper.Map<SaleResponse>(sale);
        }
    }
}
