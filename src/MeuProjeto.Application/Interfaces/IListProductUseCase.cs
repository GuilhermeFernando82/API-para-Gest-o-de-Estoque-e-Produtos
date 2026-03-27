using MeuProjeto.Application.DTOs;

namespace MeuProjeto.Application.Interfaces;

public interface IListProductUseCase
{
    Task<IEnumerable<CreateProductResponse>> ExecuteAsync();
}
