using MeuProjeto.Application.DTOs;

namespace MeuProjeto.Application.Interfaces;

public interface ICreateStockUseCase
{
    Task<CreateStockResponse> ExecuteAsync(CreateStockRequest request);
}
