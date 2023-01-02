﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CatalagoAPI.Models;

[Table("Produtos")]

public class Produto
{
    [Key]
    public int ProdutoID { get; set; }

    [Required]
    [StringLength(80)]
    public string? Name{ get; set; }

    [Required]
    [StringLength(300)]
    public string? Descricao { get; set; }

    [Column(TypeName ="decimal(10,2")]
    [Required]
    public decimal Preco { get; set; }

    [Required]
    [StringLength(300)]
    public string? ImageUrl { get; set; }


    public float Estoque { get; set; }


    public DateTime DataCadastro { get; set; }


    public int CategoriaId { get; set; }

    [JsonIgnore]
    public Categoria? Categoria{ get; set; }

}
