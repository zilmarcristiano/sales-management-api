namespace Mouts.SalesDeveloper.Application.Dtos
{
    public class PaginatedList<T> : List<T>
    {
        private List<string> items;
        private int count;
        private int v1;
        private int v2;

        public int CurrentPage { get; }
        public int TotalPages { get; }
        public int TotalCount { get; }

        private PaginatedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            AddRange(items);
        }

        public PaginatedList(List<string> items, int count, int v1, int v2)
        {
            this.items = items;
            this.count = count;
            this.v1 = v1;
            this.v2 = v2;
        }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
