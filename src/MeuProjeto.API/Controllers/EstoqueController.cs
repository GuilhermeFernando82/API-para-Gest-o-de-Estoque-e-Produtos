using Microsoft.AspNetCore.Mvc;
using MeuProjeto.Application.DTOs;
using MeuProjeto.Application.UseCases;
using Microsoft.AspNetCore.Authorization;

namespace MeuProjeto.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Administrador")]
public class EstoqueController : ControllerBase
{
    private readonly ICreateStockUseCase _createStockUseCase;

    public EstoqueController(ICreateStockUseCase createStockUseCase)
    {
        _createStockUseCase = createStockUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Adicionar([FromBody] CreateStockRequest request)
    {
        var estoque = await _createStockUseCase.ExecuteAsync(request);
        return CreatedAtAction(nameof(Adicionar), new { id = estoque.Id }, estoque);
    }
}
