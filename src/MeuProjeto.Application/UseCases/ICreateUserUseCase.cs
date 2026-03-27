using MeuProjeto.Application.DTOs;

namespace MeuProjeto.Application.UseCases;

public interface ICreateUserUseCase
{
    Task<CreateUserResponse> ExecuteAsync(CreateUserRequest request);
}
