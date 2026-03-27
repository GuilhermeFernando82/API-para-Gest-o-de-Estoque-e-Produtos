using MeuProjeto.Application.Interfaces;
using MeuProjeto.Domain;
using MeuProjeto.Domain.Repositories;

namespace MeuProjeto.Application.UseCases;

public class DeleteProductUseCase : IDeleteProductUseCase
{
    private readonly IProdutoRepository _produtoRepository;

    public DeleteProductUseCase(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task ExecuteAsync(Guid id)
    {
        var produto = await _produtoRepository.ObterPorIdAsync(id);
        if (produto == null)
            throw new KeyNotFoundException("Produto não encontrado");

        await _produtoRepository.RemoverAsync(id);
    }
}
