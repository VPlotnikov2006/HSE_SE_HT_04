using Microsoft.AspNetCore.Mvc;
using OrdersService.Application.DTOs;
using OrdersService.Application.DTOs.CreateOrder;
using OrdersService.Application.DTOs.GetOrder;
using OrdersService.Application.UseCases.CreateOrder;
using OrdersService.Application.UseCases.GetOrder;
using OrdersService.Application.UseCases.ListOrders;

namespace OrdersService.Api.Controllers;

[ApiController]
[Route("os")]
public sealed class OrdersController : ControllerBase
{
    [HttpGet("orders")]
    public async Task<ActionResult<List<OrderSummaryDto>>> ListOrders(
        [FromServices] ListOrdersHandler handler,
        CancellationToken ct)
    {
        return Ok(await handler.Handle(ct));
    }

    [HttpGet("orders/{orderId:guid}")]
    public async Task<ActionResult<OrderDto>> GetOrder(
        Guid orderId,
        [FromServices] GetOrderHandler handler,
        CancellationToken ct)
    {
        return Ok(await handler.Handle(new GetOrderQuery(orderId), ct));
    }

    [HttpPost("new-order")]
    public async Task<IActionResult> CreateOrder(
        [FromBody] CreateOrderCommand command,
        [FromServices] CreateOrderHandler handler,
        CancellationToken ct)
    {
        var result = await handler.Handle(command, ct);

        return CreatedAtAction(
            nameof(GetOrder),
            new { orderId = result.OrderId },
            result
        );
    }
}

