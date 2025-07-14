using FluentValidation;
using Microsoft.AspNetCore.Http;
using Mouts.SalesDeveloper.Application.Dtos;
using Mouts.SalesDeveloper.Domain.Validation;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Mouts.SalesDeveloper.Api.Middleware
{
    public class ValidationExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidationExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                await HandleValidationExceptionAsync(context, ex);
            }
        }

        private static Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var errors = exception.Errors.Select(error => (ValidationError)error);

            var response = ApiResult<object>.Fail("Validation failed.", errors);

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response, jsonOptions));
        }

    }
}
