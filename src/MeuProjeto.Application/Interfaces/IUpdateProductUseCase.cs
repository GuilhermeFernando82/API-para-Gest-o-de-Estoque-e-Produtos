using MeuProjeto.Application.DTOs;
using MeuProjeto.Domain.Entities;

namespace MeuProjeto.Application.Interfaces;

public interface IUpdateProductUseCase
{
    Task<CreateProductResponse> ExecuteAsync(Guid id, Produto request);
}
