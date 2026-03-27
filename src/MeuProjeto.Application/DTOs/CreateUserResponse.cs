using MeuProjeto.Domain;
using MeuProjeto.Domain.Entities;

namespace MeuProjeto.Application.DTOs;

public class CreateUserResponse
{
	public Guid Id { get; set; }
	public string Nome { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public TipoUsuario Tipo { get; set; }
}
