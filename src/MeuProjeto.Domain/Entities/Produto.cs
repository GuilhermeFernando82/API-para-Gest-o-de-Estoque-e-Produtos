namespace MeuProjeto.Domain.Entities;

public class Produto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public ICollection<Estoque> Estoques { get; set; } = new List<Estoque>();
}
