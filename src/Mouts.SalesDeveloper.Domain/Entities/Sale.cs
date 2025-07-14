using Mouts.SalesDeveloper.Domain.Common;
using Mouts.SalesDeveloper.Domain.Enums;
using Mouts.SalesDeveloper.Domain.Events;
using Mouts.SalesDeveloper.Domain.Exceptions;

namespace Mouts.SalesDeveloper.Domain.Entities
{
    public class Sale : BaseEntity
    {
        public string SaleNumber { get; set; } = string.Empty;
        public DateTime SaleDate { get; set; } = DateTime.UtcNow;
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public Guid BranchId { get; set; }
        public string BranchName { get; set; } = string.Empty;
        public ICollection<SaleItem> Items { get; set; } = new List<SaleItem>();
        public SaleStatus Status { get; set; } = SaleStatus.Pending;
        public decimal TotalAmount => Items.Sum(i => i.Total);
        public List<BaseEvent> DomainEvents { get; private set; } = new();

        public void AddItem(SaleItem item)
        {
            item.ApplyDiscount();
            Items.Add(item);
        }

        public void Cancel(string reason)
        {
            if (Status == SaleStatus.Cancelled)
                throw new DomainException("The sale is already cancelled.");

            Status = SaleStatus.Cancelled;

            DomainEvents.Add(new SaleCancelledEvent(Id, reason));
        }

        public void MarkAsCompleted()
        {
            if (!Items.Any())
                throw new DomainException("The sale must contain at least one item.");

            foreach (var item in Items)
                item.ApplyDiscount();

            Status = SaleStatus.Completed;

            DomainEvents.Add(new SaleCreatedEvent(Id, SaleDate, CustomerId, TotalAmount));
        }

        public void Modify(string saleNumber, List<SaleItem> newItems, SaleStatus status)
        {
            if (Status == SaleStatus.Cancelled)
                throw new DomainException("It is not possible to modify a cancelled sale.");

            SaleNumber = saleNumber;

            Items = new List<SaleItem>();
            foreach (var item in newItems)
            {
                item.ApplyDiscount();
                Items.Add(item);
            }

            Status = status;

            DomainEvents.Add(new SaleModifiedEvent(Id, SaleNumber, DateTime.UtcNow, Status));
        }

        public void ClearEvents()
        {
            DomainEvents.Clear();
        }

        public void CalculateDiscounts()
        {
            foreach (var item in Items)
                item.ApplyDiscount();
        }
    }
}
