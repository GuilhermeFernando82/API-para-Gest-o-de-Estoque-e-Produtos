namespace MeuProjeto.Application.DTOs;

public class CreateProductResponse
{
	public Guid Id { get; set; }
	public string Nome { get; set; } = string.Empty;
	public string Descricao { get; set; } = string.Empty;
	public decimal Preco { get; set; }
}
