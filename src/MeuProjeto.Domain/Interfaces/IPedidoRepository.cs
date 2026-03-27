using MeuProjeto.Domain.Entities;

namespace MeuProjeto.Domain.Repositories;

public interface IPedidoRepository
{
    Task<Pedido?> ObterPorIdAsync(Guid id);
    Task<IEnumerable<Pedido>> ListarAsync();
    Task AdicionarAsync(Pedido pedido);
}
