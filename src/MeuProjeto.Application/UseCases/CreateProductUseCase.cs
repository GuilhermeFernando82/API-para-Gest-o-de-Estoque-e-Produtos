using MeuProjeto.Application.DTOs;
using MeuProjeto.Domain;
using MeuProjeto.Domain.Entities;
using MeuProjeto.Domain.Repositories;

namespace MeuProjeto.Application.UseCases;

public class CreateProductUseCase : ICreateProductUseCase
{
    private readonly IProdutoRepository _produtoRepository;

    public CreateProductUseCase(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task<CreateProductResponse> ExecuteAsync(CreateProductRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Nome))
            throw new ArgumentException("Nome é obrigatório");
        if (request.Preco <= 0)
            throw new ArgumentException("Preço deve ser maior que zero");

        var produto = new Produto
        {
            Id = Guid.NewGuid(),
            Nome = request.Nome,
            Descricao = request.Descricao,
            Preco = request.Preco
        };

        await _produtoRepository.AdicionarAsync(produto);

        return new CreateProductResponse
        {
            Id = produto.Id,
            Nome = produto.Nome,
            Descricao = produto.Descricao,
            Preco = produto.Preco
        };
    }
}
