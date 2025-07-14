using Microsoft.AspNetCore.Mvc;
using Mouts.SalesDeveloper.Application.Dtos;
using Mouts.SalesDeveloper.Application.Resources;
using System.Linq;

namespace Mouts.SalesDeveloper.Api.Common
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IActionResult BadRequestResponse(string message) =>
            BadRequest(ApiResult<string>.Fail(message));

        protected IActionResult NotFoundResponse(string message = "Resource not found") =>
            NotFound(ApiResult<string>.Fail(message));

        protected IActionResult OkPaginated<T>(PaginatedList<T> pagedList) =>
            Ok(new PaginatedResult<T>
            {
                Success = true,
                Message = ApplicationMessages.OperationSuccess,
                Items = pagedList.ToList(),
                CurrentPage = pagedList.CurrentPage,
                TotalPages = pagedList.TotalPages,
                TotalCount = pagedList.TotalCount
            });

        protected IActionResult OkResponse<T>(T data, string message = "Operation completed successfully.") =>
            Ok(ApiResult<T>.Ok(data, message));

        protected IActionResult CreatedResponse<T>(string action, object routeValues, T data) =>
            CreatedAtAction(action, routeValues, ApiResult<T>.Ok(data, "Created successfully."));
    }
}
