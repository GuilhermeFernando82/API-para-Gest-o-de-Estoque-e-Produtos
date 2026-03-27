using MeuProjeto.Application.DTOs;
using MeuProjeto.Domain;
using MeuProjeto.Domain.Repositories;

namespace MeuProjeto.Application.UseCases;

public class UpdateProductUseCase : IUpdateProductUseCase
{
    private readonly IProdutoRepository _produtoRepository;

    public UpdateProductUseCase(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task<CreateProductResponse> ExecuteAsync(Guid id, CreateProductRequest request)
    {
        var produto = await _produtoRepository.ObterPorIdAsync(id);
        if (produto == null)
            throw new KeyNotFoundException("Produto não encontrado");

        if (string.IsNullOrWhiteSpace(request.Nome))
            throw new ArgumentException("Nome é obrigatório");
        if (request.Preco <= 0)
            throw new ArgumentException("Preço deve ser maior que zero");

        produto.Nome = request.Nome;
        produto.Descricao = request.Descricao;
        produto.Preco = request.Preco;

        await _produtoRepository.AtualizarAsync(produto);

        return new CreateProductResponse
        {
            Id = produto.Id,
            Nome = produto.Nome,
            Descricao = produto.Descricao,
            Preco = produto.Preco
        };
    }
}
