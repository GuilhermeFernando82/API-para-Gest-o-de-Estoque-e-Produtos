using MeuProjeto.Application.DTOs;

namespace MeuProjeto.Application.Interfaces;

public interface ICreateUserUseCase
{
    Task<CreateUserResponse> ExecuteAsync(CreateUserRequest request);
}
