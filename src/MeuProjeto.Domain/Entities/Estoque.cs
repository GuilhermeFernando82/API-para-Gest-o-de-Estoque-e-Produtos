namespace MeuProjeto.Domain.Entities;

public class Estoque
{
    public Guid Id { get; set; }
    public Guid ProdutoId { get; set; }
    public Produto? Produto { get; set; }
    public int Quantidade { get; set; }
    public string NumeroNotaFiscal { get; set; } = string.Empty;
    public DateTime DataMovimentacao { get; set; } = DateTime.UtcNow;
}
