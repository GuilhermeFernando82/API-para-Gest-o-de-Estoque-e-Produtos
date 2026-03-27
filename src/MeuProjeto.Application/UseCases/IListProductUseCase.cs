using MeuProjeto.Application.DTOs;

namespace MeuProjeto.Application.UseCases;

public interface IListProductUseCase
{
    Task<IEnumerable<CreateProductResponse>> ExecuteAsync();
}
