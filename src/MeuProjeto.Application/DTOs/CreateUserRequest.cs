using MeuProjeto.Domain;
using MeuProjeto.Domain.Entities;

namespace MeuProjeto.Application.DTOs;

public class CreateUserRequest
{
	public string Nome { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string Senha { get; set; } = string.Empty;
	public TipoUsuario Tipo { get; set; }
}
