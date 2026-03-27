namespace MeuProjeto.Application.DTOs;

public class CreateOrderRequest
{
    public string DocumentoCliente { get; set; } = string.Empty;
    public string NomeVendedor { get; set; } = string.Empty;
    public List<CreateOrderItemRequest> Itens { get; set; } = new();
}

public class CreateOrderItemRequest
{
    public Guid ProdutoId { get; set; }
    public int Quantidade { get; set; }
}
