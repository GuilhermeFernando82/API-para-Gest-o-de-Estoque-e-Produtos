namespace MeuProjeto.Domain.Entities;
public enum Categoria
{
    EquipamentosEsportivos,
    InstrumentosMusicais,
    ProdutosJardinagem,
    AcessoriosPets
}
public class Produto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty; 
    public decimal Preco { get; set; }
    public Categoria Categoria { get; set; }
}
