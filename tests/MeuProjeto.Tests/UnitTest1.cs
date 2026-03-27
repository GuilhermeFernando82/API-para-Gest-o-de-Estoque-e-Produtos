using MeuProjeto.Domain.Repositories;
using MeuProjeto.Domain.Entities;
using MeuProjeto.Application.UseCases;
using MeuProjeto.Application.DTOs;
namespace MeuProjeto.Tests;

public class CreateUserUseCaseTests
{
    private class FakeUsuarioRepository : IUsuarioRepository
    {
        public List<Usuario> Usuarios = new();

        public Task AdicionarAsync(Usuario usuario)
        {
            Usuarios.Add(usuario);
            return Task.CompletedTask;
        }

        public Task<bool> EmailExisteAsync(string email)
        {
            return Task.FromResult(Usuarios.Any(u => u.Email == email));
        }
        public Task AtualizarAsync(Usuario usuario)
        {
            var index = Usuarios.FindIndex(u => u.Id == usuario.Id);
            if (index >= 0)
                Usuarios[index] = usuario;

            return Task.CompletedTask;
        }

        public Task RemoverAsync(Guid id)
        {
            var usuario = Usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario != null)
                Usuarios.Remove(usuario);

            return Task.CompletedTask;
        }
     
        public Task<Usuario?> ObterPorEmailAsync(string email) => Task.FromResult(Usuarios.FirstOrDefault(u => u.Email == email));
        public Task<Usuario?> ObterPorIdAsync(Guid id) => Task.FromResult(Usuarios.FirstOrDefault(u => u.Id == id));
        public Task<List<Usuario>> ListarAsync() => Task.FromResult(Usuarios.ToList());
    }

    [Fact]
    public async Task ExecuteAsync_DeveCriarUsuarioComSucesso()
    {
        // Arrange
        var repo = new FakeUsuarioRepository();
        var useCase = new CreateUserUseCase(repo);
        var request = new CreateUserRequest
        {
            Nome = "Fulano",
            Email = "fulano@email.com",
            Senha = "123456",
            Tipo = TipoUsuario.Vendedor
        };

        var response = await useCase.ExecuteAsync(request);

        Assert.NotEqual(Guid.Empty, response.Id);
        Assert.Equal(request.Nome, response.Nome);
        Assert.Equal(request.Email, response.Email);
        Assert.Equal(request.Tipo, response.Tipo);
        Assert.Single(repo.Usuarios);
        Assert.Equal(request.Email, repo.Usuarios[0].Email);
    }

    [Fact]
    public async Task ExecuteAsync_DeveLancarExcecao_SeEmailDuplicado()
    {
        var repo = new FakeUsuarioRepository();
       
        await repo.AdicionarAsync(new Usuario(Guid.NewGuid(), "Fulano", "fulano@email.com", "hash", TipoUsuario.Vendedor));

        var useCase = new CreateUserUseCase(repo);
        var request = new CreateUserRequest
        {
            Nome = "Fulano",
            Email = "fulano@email.com",
            Senha = "123456",
            Tipo = TipoUsuario.Vendedor
        };

        await Assert.ThrowsAsync<InvalidOperationException>(() => useCase.ExecuteAsync(request));
        Assert.Single(repo.Usuarios);
    }
}
