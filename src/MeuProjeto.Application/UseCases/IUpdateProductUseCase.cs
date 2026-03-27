using MeuProjeto.Application.DTOs;

namespace MeuProjeto.Application.UseCases;

public interface IUpdateProductUseCase
{
    Task<CreateProductResponse> ExecuteAsync(Guid id, CreateProductRequest request);
}
