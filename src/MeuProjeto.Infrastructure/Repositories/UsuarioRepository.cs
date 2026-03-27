using Dapper;
using MeuProjeto.Domain.Entities;
using MeuProjeto.Domain.Repositories;
using MeuProjeto.Infrastructure.Database;

namespace MeuProjeto.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public UsuarioRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Usuario?> ObterPorIdAsync(Guid id)
    {
        using var conn = _connectionFactory.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Usuario>("SELECT * FROM usuarios WHERE id = @id", new { id });
    }

    public async Task<Usuario?> ObterPorEmailAsync(string email)
    {
        using var conn = _connectionFactory.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Usuario>("SELECT * FROM usuarios WHERE email = @email", new { email });
    }

    public async Task<bool> EmailExisteAsync(string email)
    {
        using var conn = _connectionFactory.CreateConnection();
        var count = await conn.ExecuteScalarAsync<int>("SELECT COUNT(1) FROM usuarios WHERE email = @email", new { email });
        return count > 0;
    }

    public async Task AdicionarAsync(Usuario usuario)
    {
        using var conn = _connectionFactory.CreateConnection();
        await conn.ExecuteAsync(@"INSERT INTO usuarios (id, nome, email, senhahash, tipo) VALUES (@Id, @Nome, @Email, @SenhaHash, @Tipo)", usuario);
    }

    public async Task AtualizarAsync(Usuario usuario)
    {
        using var conn = _connectionFactory.CreateConnection();
        await conn.ExecuteAsync(@"UPDATE usuarios SET nome = @Nome, email = @Email, senhahash = @SenhaHash, tipo = @Tipo WHERE id = @Id", usuario);
    }

    public async Task RemoverAsync(Guid id)
    {
        using var conn = _connectionFactory.CreateConnection();
        await conn.ExecuteAsync("DELETE FROM usuarios WHERE id = @id", new { id });
    }
}
