using MeuProjeto.Application.DTOs;

namespace MeuProjeto.Application.UseCases;

public interface ICreateProductUseCase
{
    Task<CreateProductResponse> ExecuteAsync(CreateProductRequest request);
}
