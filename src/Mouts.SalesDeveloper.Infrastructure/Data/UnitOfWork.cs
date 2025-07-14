using Mouts.SalesDeveloper.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Mouts.SalesDeveloper.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
