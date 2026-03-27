using MeuProjeto.Domain;
using MeuProjeto.Domain.Entities;

namespace MeuProjeto.Application.Services;

public interface IJwtService
{
    string GerarToken(Usuario usuario);
}
