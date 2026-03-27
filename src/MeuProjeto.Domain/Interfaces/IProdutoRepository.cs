using MeuProjeto.Domain.Entities;

namespace MeuProjeto.Domain.Repositories;

public interface IProdutoRepository
{
    Task<Produto?> ObterPorIdAsync(Guid id);
    Task<IEnumerable<Produto>> ListarAsync();
    Task AdicionarAsync(Produto produto);
    Task AtualizarAsync(Produto produto);
    Task RemoverAsync(Guid id);
}
