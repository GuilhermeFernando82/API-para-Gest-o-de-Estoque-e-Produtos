namespace MeuProjeto.Domain.Entities;

public enum TipoUsuario
{
    Administrador = 1,
    Vendedor = 2
}

public class Usuario
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string SenhaHash { get; set; } = string.Empty;
    public TipoUsuario Tipo { get; set; }

    public Usuario(Guid id, string nome, string email, string senha, TipoUsuario tipo)
    {
        Id = id;
        Nome = nome;
        Email = email;
        SenhaHash = senha;
        Tipo = tipo;
    }
    public Usuario() { }
}
