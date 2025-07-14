namespace Mouts.SalesDeveloper.Domain.Repositories
{

    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}