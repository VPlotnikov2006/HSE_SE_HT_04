using Microsoft.AspNetCore.Mvc;
using PaymentsService.Application.DTOs;
using PaymentsService.Application.UseCases;

namespace PaymentsService.API.Controllers;

[ApiController]
[Route("ps")]
public class PaymentsController : ControllerBase
{
    [HttpGet("balance/{userId:guid}")]
    public async Task<ActionResult<GetBalanceResult>> GetBalance(
        Guid userId,
        [FromServices] GetBalanceHandler handler,
        CancellationToken ct)
    {
        return Ok(await handler.Handle(userId, ct));
    }

    [HttpPut("deposit")]
    public async Task<IActionResult> Deposit(
        [FromBody] UpdateBalanceCommand command,
        [FromServices] DepositHandler handler,
        CancellationToken ct)
    {
        await handler.Handle(command, ct);
        return NoContent();
    }

    [HttpPost("create-account")]
    public async Task<IActionResult> CreateAccount(
        [FromBody] CreateAccountCommand command,
        [FromServices] CreateAccountHandler handler,
        CancellationToken ct)
    {
        await handler.Handle(command, ct);
        return CreatedAtAction(
            nameof(GetBalance),
            new { userId = command.UserId },
            null
        );
    }
}