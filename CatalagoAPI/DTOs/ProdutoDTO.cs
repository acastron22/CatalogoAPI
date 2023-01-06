using CatalagoAPI.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CatalagoAPI.DTOs
{
    public class ProdutoDTO
    {
        public int ProdutoID { get; set; }

        public string? Name { get; set; }

        public string? Descricao { get; set; }

        public decimal Preco { get; set; }

        public string? ImageUrl { get; set; }

        public int CategoriaId { get; set; }

    }
}
