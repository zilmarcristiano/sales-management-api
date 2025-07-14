namespace Mouts.SalesDeveloper.Tests.Shared
{
    public static class DateTimeHelper
    {
        public static DateTime UtcNow => DateTime.UtcNow;
        public static DateTime FixedDate => new DateTime(2025, 01, 01, 12, 0, 0, DateTimeKind.Utc);
    }
}
