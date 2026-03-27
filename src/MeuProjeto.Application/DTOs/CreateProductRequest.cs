namespace MeuProjeto.Application.DTOs;

public class CreateProductRequest
{
	public string Nome { get; set; } = string.Empty;
	public string Descricao { get; set; } = string.Empty;
	public decimal Preco { get; set; }
}
