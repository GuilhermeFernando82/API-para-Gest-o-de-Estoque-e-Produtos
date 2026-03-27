using Microsoft.AspNetCore.Mvc;
using MeuProjeto.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using MeuProjeto.Domain.Entities;
using MeuProjeto.Application.Interfaces;

namespace MeuProjeto.API.Controllers;

[ApiController]
[Route("api/[controller]")]
// [Authorize(Roles = "Administrador")]
public class ProdutoController : ControllerBase
{
    private readonly ICreateProductUseCase _createProductUseCase;
    private readonly IUpdateProductUseCase _updateProductUseCase;
    private readonly IListProductUseCase _listProductUseCase;
    private readonly IDeleteProductUseCase _deleteProductUseCase;

    public ProdutoController(
        ICreateProductUseCase createProductUseCase,
        IUpdateProductUseCase updateProductUseCase,
        IListProductUseCase listProductUseCase,
        IDeleteProductUseCase deleteProductUseCase)
    {
        _createProductUseCase = createProductUseCase;
        _updateProductUseCase = updateProductUseCase;
        _listProductUseCase = listProductUseCase;
        _deleteProductUseCase = deleteProductUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var produtos = await _listProductUseCase.ExecuteAsync();
        return Ok(produtos);
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] Produto request)
    {
        var produto = await _createProductUseCase.ExecuteAsync(request);
        return CreatedAtAction(nameof(ObterPorId), new { id = produto.Id }, produto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        var produtos = await _listProductUseCase.ExecuteAsync();
        var produto = produtos.FirstOrDefault(p => p.Id == id);
        if (produto == null) return NotFound();
        return Ok(produto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Editar(Guid id, [FromBody] Produto request)
    {
        var produto = await _updateProductUseCase.ExecuteAsync(id, request);
        return Ok(produto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(Guid id)
    {
        await _deleteProductUseCase.ExecuteAsync(id);
        return NoContent();
    }
}
