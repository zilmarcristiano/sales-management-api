namespace Mouts.SalesDeveloper.Domain.DomainValidation
{
    public interface IValidator<T>
    {
        void Validate(T entity);
    }
}
