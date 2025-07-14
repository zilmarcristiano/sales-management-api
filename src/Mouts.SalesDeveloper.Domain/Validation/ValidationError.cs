using FluentValidation.Results;

namespace Mouts.SalesDeveloper.Domain.Validation
{

    public class ValidationError
    {
        public string Error { get; init; } = string.Empty;
        public string Detail { get; init; } = string.Empty;

        public static explicit operator ValidationError(ValidationFailure failure)
        {
            return new ValidationError
            {
                Error = failure.PropertyName,
                Detail = failure.ErrorMessage
            };
        }
    }
}
