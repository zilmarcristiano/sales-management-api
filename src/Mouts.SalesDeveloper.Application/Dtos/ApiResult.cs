using Mouts.SalesDeveloper.Domain.Validation;
using System.Text.Json.Serialization;

namespace Mouts.SalesDeveloper.Application.Dtos
{
    public class ApiResult<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<ValidationError>? Errors { get; set; } = [];

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Data { get; set; }

        public static ApiResult<T> Ok(T data, string message = "Operation completed successfully.") =>
            new() { Success = true, Message = message, Data = data };

        public static ApiResult<T> Ok(string message = "Operation completed successfully.") =>
            new() { Success = true, Message = message };

        public static ApiResult<T> Fail(string message, IEnumerable<ValidationError>? errors = null) =>
            new() { Success = false, Message = message, Errors = errors ?? [] };
    }
}
