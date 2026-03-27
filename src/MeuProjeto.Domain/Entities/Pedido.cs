namespace MeuProjeto.Domain.Entities;

public class Pedido
{
    public Guid Id { get; set; }
    public string DocumentoCliente { get; set; } = string.Empty;
    public string NomeVendedor { get; set; } = string.Empty;
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    public ICollection<ItemPedido> Itens { get; set; } = new List<ItemPedido>();
}

public class ItemPedido
{
    public Guid Id { get; set; }
    public Guid PedidoId { get; set; }
    public Pedido? Pedido { get; set; }
    public Guid ProdutoId { get; set; }
    public Produto? Produto { get; set; }
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
}
