namespace Mouts.SalesDeveloper.Application.Dtos
{
    public class PaginatedResult<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<T> Items { get; set; } = [];
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }

        public static PaginatedResult<T> Ok(List<T> items, int page, int totalPages, int totalCount, string message = "Operation completed successfully.")
        {
            return new PaginatedResult<T>
            {
                Success = true,
                Message = message,
                Items = items,
                CurrentPage = page,
                TotalPages = totalPages,
                TotalCount = totalCount
            };
        }
    }
}
