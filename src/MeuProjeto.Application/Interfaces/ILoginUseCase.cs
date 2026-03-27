using MeuProjeto.Application.DTOs;

namespace MeuProjeto.Application.Interfaces;

public interface ILoginUseCase
{
    Task<LoginResponse> ExecuteAsync(LoginRequest request);
}
