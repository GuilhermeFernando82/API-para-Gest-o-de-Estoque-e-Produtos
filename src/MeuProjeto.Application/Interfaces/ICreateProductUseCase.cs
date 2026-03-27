using MeuProjeto.Application.DTOs;
using MeuProjeto.Domain.Entities;

namespace MeuProjeto.Application.Interfaces;

public interface ICreateProductUseCase
{
    Task<CreateProductResponse> ExecuteAsync(Produto request);
}
