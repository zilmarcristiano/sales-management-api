using FluentValidation;

namespace Mouts.SalesDeveloper.Domain.Validation
{

    public static class Validator
    {

        public static async Task<IEnumerable<ValidationError>> ValidateAsync<T>(T instance, IValidator<T> validator)
        {
            var result = await validator.ValidateAsync(instance);

            return result.IsValid
                ? []
                : result.Errors.Select(e => (ValidationError)e);
        }
    }
}
