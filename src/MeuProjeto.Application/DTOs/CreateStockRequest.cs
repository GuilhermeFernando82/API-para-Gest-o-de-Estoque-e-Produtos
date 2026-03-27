namespace MeuProjeto.Application.DTOs;

public class CreateStockRequest
{
    public Guid ProdutoId { get; set; }
    public int Quantidade { get; set; }
    public string NumeroNotaFiscal { get; set; } = string.Empty;
}
