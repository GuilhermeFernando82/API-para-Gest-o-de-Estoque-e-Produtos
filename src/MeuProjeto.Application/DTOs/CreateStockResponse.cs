namespace MeuProjeto.Application.DTOs;

public class CreateStockResponse
{
    public Guid Id { get; set; }
    public Guid ProdutoId { get; set; }
    public int Quantidade { get; set; }
    public string NumeroNotaFiscal { get; set; } = string.Empty;
    public DateTime DataMovimentacao { get; set; }
}
