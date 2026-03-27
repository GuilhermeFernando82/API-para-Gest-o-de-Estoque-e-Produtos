using MeuProjeto.Application.DTOs;

namespace MeuProjeto.Application.UseCases;

public interface ILoginUseCase
{
    Task<LoginResponse> ExecuteAsync(LoginRequest request);
}
