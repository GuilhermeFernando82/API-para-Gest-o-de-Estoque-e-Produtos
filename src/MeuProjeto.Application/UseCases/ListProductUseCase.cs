using MeuProjeto.Application.DTOs;
using MeuProjeto.Domain;
using MeuProjeto.Domain.Repositories;

namespace MeuProjeto.Application.UseCases;

public class ListProductUseCase : IListProductUseCase
{
    private readonly IProdutoRepository _produtoRepository;

    public ListProductUseCase(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task<IEnumerable<CreateProductResponse>> ExecuteAsync()
    {
        var produtos = await _produtoRepository.ListarAsync();
        return produtos.Select(p => new CreateProductResponse
        {
            Id = p.Id,
            Nome = p.Nome,
            Descricao = p.Descricao,
            Preco = p.Preco
        });
    }
}
