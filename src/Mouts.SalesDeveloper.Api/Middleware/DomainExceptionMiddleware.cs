using Microsoft.AspNetCore.Http;
using Mouts.SalesDeveloper.Domain.Exceptions;
using Mouts.SalesDeveloper.Application.Dtos;
using System.Text.Json;
using System.Threading.Tasks;

namespace Mouts.SalesDeveloper.Api.Middleware
{
    public class DomainExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public DomainExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DomainException ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var response = ApiResult<object>.Fail(ex.Message);

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
            }
        }
    }
}
