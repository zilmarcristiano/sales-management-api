using FluentValidation.Results;

namespace Mouts.SalesDeveloper.Domain.Validation
{

    public class ValidationResultDetail
    {
        public bool IsValid { get; set; }
        public IEnumerable<ValidationError> Errors { get; set; } = [];

        public ValidationResultDetail()
        {

        }

        public ValidationResultDetail(ValidationResult validationResult)
        {
            IsValid = validationResult.IsValid;
            Errors = validationResult.Errors.Select(o => (ValidationError)o);
        }
    }
}