using Microsoft.AspNetCore.Mvc;
using MeuProjeto.Application.DTOs;
using MeuProjeto.Application.UseCases;
using Microsoft.AspNetCore.Authorization;

namespace MeuProjeto.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Vendedor")]
public class PedidoController : ControllerBase
{
    private readonly ICreateOrderUseCase _createOrderUseCase;

    public PedidoController(ICreateOrderUseCase createOrderUseCase)
    {
        _createOrderUseCase = createOrderUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CreateOrderRequest request)
    {
        var pedido = await _createOrderUseCase.ExecuteAsync(request);
        return CreatedAtAction(nameof(Criar), new { id = pedido.Id }, pedido);
    }
}
