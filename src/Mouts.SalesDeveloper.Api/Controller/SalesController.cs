using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mouts.SalesDeveloper.Api.Common;
using Mouts.SalesDeveloper.Application.Dtos;
using Mouts.SalesDeveloper.Application.Sales.Commands;
using Mouts.SalesDeveloper.Application.Sales.Queries;
using System;
using System.Threading.Tasks;

namespace Mouts.SalesDeveloper.Api.Controller
{
    [ApiController]
    [Route("api/sales")]
    public class SaleController : BaseController
    {
        private readonly IMediator _mediator;

        public SaleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetSaleByIdQuery(id));

            if (result is null)
                return NotFoundResponse("Sale not found.");

            return OkResponse(result, "Sale retrieved successfully.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllSalesQuery());
            return OkResponse(new { Sales = result }, "Sales list retrieved successfully.");
        }

        [HttpGet("paginated")]
        public async Task<IActionResult> GetPaginated([FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            var result = await _mediator.Send(new GetPaginatedSalesQuery(page, size));

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SaleRequest request)
        {
            var result = await _mediator.Send(new CreateSaleCommand(request));
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, ApiResult<SaleResponse>.Ok(result, "Sale created successfully."));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] SaleRequest request)
        {
            var result = await _mediator.Send(new UpdateSaleCommand(id, request));
            return OkResponse(result, "Sale updated successfully.");
        }

        [HttpPost("{id:guid}/cancel")]
        public async Task<IActionResult> Cancel(Guid id, [FromBody] CancelSaleRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Reason))
                return BadRequestResponse("The cancellation reason must be provided.");

            await _mediator.Send(new CancelSaleCommand(id, request.Reason));
            return OkResponse<string>(null, "Sale cancelled successfully.");
        }
    }
}
