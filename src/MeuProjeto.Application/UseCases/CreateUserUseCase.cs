using MeuProjeto.Application.DTOs;
using MeuProjeto.Application.Interfaces;
using MeuProjeto.Domain;
using MeuProjeto.Domain.Entities;
using MeuProjeto.Domain.Repositories;

namespace MeuProjeto.Application.UseCases;

public class CreateUserUseCase : ICreateUserUseCase
{
    private readonly IUsuarioRepository _usuarioRepository;

    public CreateUserUseCase(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<CreateUserResponse> ExecuteAsync(CreateUserRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Nome))
            throw new ArgumentException("Nome é obrigatório");
        if (string.IsNullOrWhiteSpace(request.Email))
            throw new ArgumentException("Email é obrigatório");
        if (string.IsNullOrWhiteSpace(request.Senha) || request.Senha.Length < 6)
            throw new ArgumentException("Senha deve ter pelo menos 6 caracteres");

        if (await _usuarioRepository.EmailExisteAsync(request.Email))
            throw new InvalidOperationException("Email já cadastrado");

        var senhaHash = BCrypt.Net.BCrypt.HashPassword(request.Senha);

        var usuario = new Usuario
        {
            Id = Guid.NewGuid(),
            Nome = request.Nome,
            Email = request.Email,
            SenhaHash = senhaHash,
            Tipo = request.Tipo
        };

        await _usuarioRepository.AdicionarAsync(usuario);

        return new CreateUserResponse
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            Tipo = usuario.Tipo
        };
    }
}
