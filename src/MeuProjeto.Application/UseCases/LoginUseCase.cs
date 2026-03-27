using MeuProjeto.Application.DTOs;
using MeuProjeto.Application.Services;
using MeuProjeto.Domain;
using MeuProjeto.Domain.Repositories;

namespace MeuProjeto.Application.UseCases;

public class LoginUseCase : ILoginUseCase
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IJwtService _jwtService;

    public LoginUseCase(IUsuarioRepository usuarioRepository, IJwtService jwtService)
    {
        _usuarioRepository = usuarioRepository;
        _jwtService = jwtService;
    }

    public async Task<LoginResponse> ExecuteAsync(LoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Senha))
            throw new ArgumentException("Email e senha são obrigatórios");

        var usuario = await _usuarioRepository.ObterPorEmailAsync(request.Email);

        if (usuario is null)
            throw new UnauthorizedAccessException("Usuário ou senha inválidos");

        if (!BCrypt.Net.BCrypt.Verify(request.Senha, usuario.SenhaHash))
            throw new UnauthorizedAccessException("Usuário ou senha inválidos");

        var token = _jwtService.GerarToken(usuario!);

        return new LoginResponse { Token = token };
    }
}
