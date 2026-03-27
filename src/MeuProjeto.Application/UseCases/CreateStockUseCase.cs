using MeuProjeto.Application.DTOs;
using MeuProjeto.Application.Interfaces;
using MeuProjeto.Domain;
using MeuProjeto.Domain.Entities;
using MeuProjeto.Domain.Repositories;

namespace MeuProjeto.Application.UseCases;

public class CreateStockUseCase : ICreateStockUseCase
{
    private readonly IEstoqueRepository _estoqueRepository;
    private readonly IProdutoRepository _produtoRepository;

    public CreateStockUseCase(IEstoqueRepository estoqueRepository, IProdutoRepository produtoRepository)
    {
        _estoqueRepository = estoqueRepository;
        _produtoRepository = produtoRepository;
    }

    public async Task<CreateStockResponse> ExecuteAsync(CreateStockRequest request)
    {
        var produto = await _produtoRepository.ObterPorIdAsync(request.ProdutoId);
        if (produto == null)
            throw new KeyNotFoundException("Produto não encontrado");
        if (request.Quantidade <= 0)
            throw new ArgumentException("Quantidade deve ser maior que zero");
        if (string.IsNullOrWhiteSpace(request.NumeroNotaFiscal))
            throw new ArgumentException("Número da nota fiscal é obrigatório");

        var estoque = new Estoque
        {
            Id = Guid.NewGuid(),
            ProdutoId = request.ProdutoId,
            Quantidade = request.Quantidade,
            NumeroNotaFiscal = request.NumeroNotaFiscal,
            DataMovimentacao = DateTime.UtcNow
        };

        await _estoqueRepository.AdicionarAsync(estoque);

        return new CreateStockResponse
        {
            Id = estoque.Id,
            ProdutoId = estoque.ProdutoId,
            Quantidade = estoque.Quantidade,
            NumeroNotaFiscal = estoque.NumeroNotaFiscal,
            DataMovimentacao = estoque.DataMovimentacao
        };
    }
}
