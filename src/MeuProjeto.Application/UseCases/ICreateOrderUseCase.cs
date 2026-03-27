using MeuProjeto.Application.DTOs;

namespace MeuProjeto.Application.UseCases;

public interface ICreateOrderUseCase
{
    Task<CreateOrderResponse> ExecuteAsync(CreateOrderRequest request);
}
