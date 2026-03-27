using Dapper;
using MeuProjeto.Domain.Entities;
using MeuProjeto.Domain.Repositories;
using MeuProjeto.Infrastructure.Database;

namespace MeuProjeto.Infrastructure.Repositories;

public class PedidoRepository : IPedidoRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public PedidoRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Pedido?> ObterPorIdAsync(Guid id)
    {
        using var conn = _connectionFactory.CreateConnection();
        var pedido = await conn.QueryFirstOrDefaultAsync<Pedido>("SELECT * FROM pedidos WHERE id = @id", new { id });
        if (pedido == null) return null;
        var itens = (await conn.QueryAsync<ItemPedido>("SELECT * FROM itenspedido WHERE pedidoid = @pedidoId", new { pedidoId = id })).ToList();
        pedido.Itens = itens;
        return pedido;
    }

    public async Task<IEnumerable<Pedido>> ListarAsync()
    {
        using var conn = _connectionFactory.CreateConnection();
        var pedidos = (await conn.QueryAsync<Pedido>("SELECT * FROM pedidos")).ToList();
        foreach (var pedido in pedidos)
        {
            var itens = (await conn.QueryAsync<ItemPedido>("SELECT * FROM itenspedido WHERE pedidoid = @pedidoId", new { pedidoId = pedido.Id })).ToList();
            pedido.Itens = itens;
        }
        return pedidos;
    }

    public async Task AdicionarAsync(Pedido pedido)
    {
        using var conn = _connectionFactory.CreateConnection();
        await conn.ExecuteAsync(@"INSERT INTO pedidos (id, documentocliente, nomevendedor, datacriacao) VALUES (@Id, @DocumentoCliente, @NomeVendedor, @DataCriacao)", pedido);
        foreach (var item in pedido.Itens)
        {
            await conn.ExecuteAsync(@"INSERT INTO itenspedido (id, pedidoid, produtoid, quantidade, precounitario) VALUES (@Id, @PedidoId, @ProdutoId, @Quantidade, @PrecoUnitario)", item);
        }
    }
}
