namespace CatalagoAPI.Models;

public class Produto
{
    public int ProdutoID { get; set; }
    public string? Name{ get; set; }
    public string? Descricao { get; set; }
    public decimal Preco { get; set; }
    public string? ImageUrl { get; set; }
    public float Estoque { get; set; }
    public DateTime DataCadastro { get; set; }
    public int CategoriaId { get; set; }
    public Categoria? Categoria{ get; set; }

}
