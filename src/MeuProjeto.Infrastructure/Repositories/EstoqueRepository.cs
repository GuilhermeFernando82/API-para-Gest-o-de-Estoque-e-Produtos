using Dapper;
using MeuProjeto.Domain;
using MeuProjeto.Domain.Repositories;
using MeuProjeto.Infrastructure.Database;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using MeuProjeto.Domain.Entities;

namespace MeuProjeto.Infrastructure.Repositories;

public class EstoqueRepository : IEstoqueRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public EstoqueRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Estoque?> ObterPorIdAsync(Guid id)
    {
        using var conn = _connectionFactory.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Estoque>("SELECT * FROM estoques WHERE id = @id", new { id });
    }

    public async Task<IEnumerable<Estoque>> ListarPorProdutoAsync(Guid produtoId)
    {
        using var conn = _connectionFactory.CreateConnection();
        return await conn.QueryAsync<Estoque>("SELECT * FROM estoques WHERE produtoid = @produtoId", new { produtoId });
    }

    public async Task AdicionarAsync(Estoque estoque)
    {
        using var conn = _connectionFactory.CreateConnection();
        await conn.ExecuteAsync(@"INSERT INTO estoques (id, produtoid, quantidade, numeronotafiscal, datamovimentacao) VALUES (@Id, @ProdutoId, @Quantidade, @NumeroNotaFiscal, @DataMovimentacao)", estoque);
    }

    public async Task AtualizarAsync(Estoque estoque)
    {
        using var conn = _connectionFactory.CreateConnection();
        await conn.ExecuteAsync(@"UPDATE estoques SET quantidade = @Quantidade, numeronotafiscal = @NumeroNotaFiscal, datamovimentacao = @DataMovimentacao WHERE id = @Id", estoque);
    }

    public async Task RemoverAsync(Guid id)
    {
        using var conn = _connectionFactory.CreateConnection();
        await conn.ExecuteAsync("DELETE FROM estoques WHERE id = @id", new { id });
    }

    public async Task<int> ObterQuantidadeDisponivelAsync(Guid produtoId)
    {
        using var conn = _connectionFactory.CreateConnection();
        var result = await conn.ExecuteScalarAsync<int>("SELECT COALESCE(SUM(quantidade),0) FROM estoques WHERE produtoid = @produtoId", new { produtoId });
        return result;
    }
}
