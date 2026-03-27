using MeuProjeto.Application.DTOs;

namespace MeuProjeto.Application.Interfaces;

public interface ICreateOrderUseCase
{
    Task<CreateOrderResponse> ExecuteAsync(CreateOrderRequest request);
}
