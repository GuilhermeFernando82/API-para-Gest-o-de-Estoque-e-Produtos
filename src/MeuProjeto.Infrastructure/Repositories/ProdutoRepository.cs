using Dapper;
using MeuProjeto.Domain.Entities;
using MeuProjeto.Domain.Repositories;
using MeuProjeto.Infrastructure.Database;

namespace MeuProjeto.Infrastructure.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public ProdutoRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Produto?> ObterPorIdAsync(Guid id)
    {
        using var conn = _connectionFactory.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Produto>("SELECT * FROM produtos WHERE id = @id", new { id });
    }

    public async Task<IEnumerable<Produto>> ListarAsync()
    {
        using var conn = _connectionFactory.CreateConnection();
        return await conn.QueryAsync<Produto>("SELECT * FROM produtos");
    }

    public async Task AdicionarAsync(Produto produto)
    {
        using var conn = _connectionFactory.CreateConnection();

        await conn.ExecuteAsync(@"
        INSERT INTO produtos 
        (id, nome, descricao, preco, categoria) 
        VALUES 
        (@Id, @Nome, @Descricao, @Preco, @Categoria)",
            new
            {
                produto.Id,
                produto.Nome,
                produto.Descricao,
                produto.Preco,
                Categoria = produto.Categoria.ToString()
            });
    }
    public async Task AtualizarAsync(Produto produto)
    {
        using var conn = _connectionFactory.CreateConnection();

        await conn.ExecuteAsync(@"
        UPDATE produtos 
        SET 
            nome = @Nome, 
            descricao = @Descricao, 
            preco = @Preco,
            categoria = @Categoria
        WHERE id = @Id",
            new
            {
                produto.Id,
                produto.Nome,
                produto.Descricao,
                produto.Preco,
                Categoria = produto.Categoria.ToString()
            });
    }

    public async Task RemoverAsync(Guid id)
    {
        using var conn = _connectionFactory.CreateConnection();
        await conn.ExecuteAsync("DELETE FROM produtos WHERE id = @id", new { id });
    }
}
