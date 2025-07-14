namespace Mouts.SalesDeveloper.Application.Resources
{
    public static class ValidationMessages
    {
        public const string SaleNumberRequired = "The sale number is required.";
        public const string SaleDateInFuture = "The sale date cannot be in the future.";
        public const string CustomerRequired = "The customer is required.";
        public const string SaleMustHaveItems = "The sale must contain at least one item.";
        public const string ProductRequired = "The product is required.";
        public const string QuantityGreaterThanZero = "Quantity must be greater than zero.";
        public const string QuantityLimit = "The maximum allowed quantity per item is 20 units.";
        public const string UnitPriceGreaterThanZero = "The unit price must be greater than zero.";
        public const string SaleIdRequired = "The sale ID is required.";
        public const string CancellationReasonRequired = "The cancellation reason is required.";
        public const string CancellationReasonDetailed = "Please provide a more detailed reason.";
    }
}
