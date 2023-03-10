using CatalagoAPI.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CatalagoAPI.Models;

[Table("Produtos")]

public class Produto : IValidatableObject
{
    [Key]
    public int ProdutoID { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(20, ErrorMessage = "O nome deve ter entre 5 e 20 caracteres", MinimumLength = 5)]
    //[PrimeiraLetraMaiuscula]
    public string? Name{ get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "A descrição deve ter no máximo {1} caracteres")]
    public string? Descricao { get; set; }

    [Required]
    [DataType(DataType.Currency)]
    [Column(TypeName ="decimal(8,2)")]
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

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!string.IsNullOrEmpty(this.Name))
        {
            var primeiraLetra = this.Name[0].ToString();

            if (primeiraLetra != primeiraLetra.ToUpper())
            {
                yield return new
                    ValidationResult("A primeira letra do produto deve ser maiúscula",
                    new[]
                    { nameof(this.Name) }
                    );
            }

            if (this.Estoque <= 0)
            {
                yield return new
                    ValidationResult("O estoque deve ser maior que 0",
                    new[]
                    {
                        nameof(this.Estoque) }
                    );
            }
        }
    }
}
