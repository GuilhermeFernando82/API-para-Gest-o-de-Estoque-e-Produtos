using MeuProjeto.Application.DTOs;
using MeuProjeto.Application.Interfaces;
using MeuProjeto.Domain;
using MeuProjeto.Domain.Entities;
using MeuProjeto.Domain.Repositories;

namespace MeuProjeto.Application.UseCases;

public class CreateOrderUseCase : ICreateOrderUseCase
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IProdutoRepository _produtoRepository;
    private readonly IEstoqueRepository _estoqueRepository;

    public CreateOrderUseCase(IPedidoRepository pedidoRepository, IProdutoRepository produtoRepository, IEstoqueRepository estoqueRepository)
    {
        _pedidoRepository = pedidoRepository;
        _produtoRepository = produtoRepository;
        _estoqueRepository = estoqueRepository;
    }

    public async Task<CreateOrderResponse> ExecuteAsync(CreateOrderRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.DocumentoCliente))
            throw new ArgumentException("Documento do cliente é obrigatório");
        if (string.IsNullOrWhiteSpace(request.NomeVendedor))
            throw new ArgumentException("Nome do vendedor é obrigatório");
        if (request.Itens == null || !request.Itens.Any())
            throw new ArgumentException("Pedido deve conter ao menos um item");

        var pedido = new Pedido
        {
            Id = Guid.NewGuid(),
            DocumentoCliente = request.DocumentoCliente,
            NomeVendedor = request.NomeVendedor,
            DataCriacao = DateTime.UtcNow,
            Itens = new List<ItemPedido>()
        };

        foreach (var item in request.Itens)
        {
            var produto = await _produtoRepository.ObterPorIdAsync(item.ProdutoId);
            if (produto == null)
                throw new ArgumentException($"Produto {item.ProdutoId} não encontrado");

            var estoqueDisponivel = await _estoqueRepository.ObterQuantidadeDisponivelAsync(item.ProdutoId);
            if (estoqueDisponivel < item.Quantidade)
                throw new InvalidOperationException($"Estoque insuficiente para o produto {produto.Nome}");

            var precoUnitario = produto.Preco;

            pedido.Itens.Add(new ItemPedido
            {
                Id = Guid.NewGuid(),
                PedidoId = pedido.Id,
                ProdutoId = item.ProdutoId,
                Quantidade = item.Quantidade,
                PrecoUnitario = precoUnitario
            });

            // Baixar estoque
            var estoques = await _estoqueRepository.ListarPorProdutoAsync(item.ProdutoId);
            var quantidadeRestante = item.Quantidade;
            foreach (var estoque in estoques.OrderBy(e => e.DataMovimentacao))
            {
                if (estoque.Quantidade >= quantidadeRestante)
                {
                    estoque.Quantidade -= quantidadeRestante;
                    await _estoqueRepository.AtualizarAsync(estoque);
                    break;
                }
                else
                {
                    quantidadeRestante -= estoque.Quantidade;
                    estoque.Quantidade = 0;
                    await _estoqueRepository.AtualizarAsync(estoque);
                }
            }
        }

        await _pedidoRepository.AdicionarAsync(pedido);

        return new CreateOrderResponse
        {
            Id = pedido.Id,
            DocumentoCliente = pedido.DocumentoCliente,
            NomeVendedor = pedido.NomeVendedor,
            DataCriacao = pedido.DataCriacao,
            Itens = pedido.Itens.Select(i => new CreateOrderItemResponse
            {
                ProdutoId = i.ProdutoId,
                Quantidade = i.Quantidade,
                PrecoUnitario = i.PrecoUnitario
            }).ToList()
        };
    }
}
