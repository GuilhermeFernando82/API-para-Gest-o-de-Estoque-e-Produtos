using MeuProjeto.Domain.Entities;

namespace MeuProjeto.Domain.Repositories;

public interface IEstoqueRepository
{
    Task<Estoque?> ObterPorIdAsync(Guid id);
    Task<IEnumerable<Estoque>> ListarPorProdutoAsync(Guid produtoId);
    Task AdicionarAsync(Estoque estoque);
    Task AtualizarAsync(Estoque estoque);
    Task RemoverAsync(Guid id);
    Task<int> ObterQuantidadeDisponivelAsync(Guid produtoId);
}
