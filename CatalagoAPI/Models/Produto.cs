using CatalagoAPI.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CatalagoAPI.Models;

[Table("Produtos")]

public class Produto
{
    [Key]
    public int ProdutoID { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(20, ErrorMessage = "O nome deve ter entre 5 e 20 caracteres", MinimumLength = 5)]
    [PrimeiraLetraMaiuscula]
    public string? Name{ get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "A descrição deve ter no máximo {1} caracteres")]
    public string? Descricao { get; set; }

    [Column(TypeName ="decimal(10,2")]
    [Required]
    [Range (1, 10000, ErrorMessage = "O preço deve estar entre {1} e {2}")]
    public decimal Preco { get; set; }

    [Required]
    [StringLength(300, MinimumLength = 10)]
    public string? ImageUrl { get; set; }


    public float Estoque { get; set; }


    public DateTime DataCadastro { get; set; }


    public int CategoriaId { get; set; }

    [JsonIgnore]
    public Categoria? Categoria{ get; set; }

}
