namespace MeuProjeto.Application.DTOs;

public class CreateOrderResponse
{
    public Guid Id { get; set; }
    public string DocumentoCliente { get; set; } = string.Empty;
    public string NomeVendedor { get; set; } = string.Empty;
    public DateTime DataCriacao { get; set; }
    public List<CreateOrderItemResponse> Itens { get; set; } = new();
}

public class CreateOrderItemResponse
{
    public Guid ProdutoId { get; set; }
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
}
