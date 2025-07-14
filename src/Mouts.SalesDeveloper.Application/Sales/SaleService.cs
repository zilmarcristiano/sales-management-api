using Mouts.SalesDeveloper.Domain.Entities;
using Mouts.SalesDeveloper.Domain.Repositories;
using Mouts.SalesDeveloper.Domain.DomainValidation;
using Mouts.SalesDeveloper.Application.Dtos;
using Mouts.SalesDeveloper.Application.Services;
using Mouts.SalesDeveloper.Domain.Exceptions;
using Mouts.SalesDeveloper.Domain.Resources;

namespace Mouts.SalesDeveloper.Application.Sales
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _repository;
        private readonly IValidator<Sale> _saleValidator;
        private readonly IValidator<SaleItem> _itemValidator;

        public SaleService(
            ISaleRepository repository,
            IValidator<Sale> saleValidator,
            IValidator<SaleItem> itemValidator)
        {
            _repository = repository;
            _saleValidator = saleValidator;
            _itemValidator = itemValidator;
        }

        public async Task<Sale> CreateAsync(Sale sale)
        {
            ValidateSale(sale);
            sale.MarkAsCompleted();
            await _repository.AddAsync(sale);
            PublishEvents(sale);
            return sale;
        }

        public async Task<Sale> UpdateAsync(Sale sale)
        {
            ValidateSale(sale);
            sale.Modify(sale.SaleNumber, sale.Items.ToList(), sale.Status);
            await _repository.UpdateAsync(sale);
            PublishEvents(sale);
            return sale;
        }

        public async Task CancelAsync(Guid saleId, string reason)
        {
            var sale = await _repository.GetByIdAsync(saleId)
                ?? throw new DomainException(DomainMessages.SaleNotFound);

            sale.Cancel(reason);

            await _repository.UpdateAsync(sale);

            PublishEvents(sale);
        }

        private void ValidateSale(Sale sale)
        {
            _saleValidator.Validate(sale);

            foreach (var item in sale.Items)
                _itemValidator.Validate(item);
        }

        private void PublishEvents(Sale sale)
        {
            foreach (var e in sale.DomainEvents)
                Console.WriteLine(string.Format(DomainMessages.EventPublished, e.GetType().Name));

            sale.ClearEvents();
        }

        public async Task<Sale?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Sale>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<PaginatedList<Sale>> GetPaginatedAsync(int pageNumber, int pageSize)
        {
            var query = await _repository.GetQueryableAsync();
            return await PaginatedList<Sale>.CreateAsync(query, pageNumber, pageSize);
        }
    }
}
