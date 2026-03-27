using MeuProjeto.Application.DTOs;

namespace MeuProjeto.Application.UseCases;

public interface ICreateStockUseCase
{
    Task<CreateStockResponse> ExecuteAsync(CreateStockRequest request);
}
