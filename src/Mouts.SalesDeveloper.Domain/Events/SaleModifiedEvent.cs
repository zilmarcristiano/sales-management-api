using Mouts.SalesDeveloper.Domain.Enums;

namespace Mouts.SalesDeveloper.Domain.Events
{
    public class SaleModifiedEvent : BaseEvent
    {
        public Guid SaleId { get; }
        public string SaleNumber { get; }
        public DateTime ModifiedAt { get; }
        public SaleStatus Status { get; set; }
        public SaleModifiedEvent(Guid saleId, string saleNumber, DateTime modifiedAt, SaleStatus status)
        {
            SaleId = saleId;
            SaleNumber = saleNumber;
            ModifiedAt = modifiedAt;
            Status = status;
        }
    }
}
