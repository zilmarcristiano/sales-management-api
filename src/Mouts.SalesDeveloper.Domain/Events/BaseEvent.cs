namespace Mouts.SalesDeveloper.Domain.Events
{
    public abstract class BaseEvent
    {
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}
