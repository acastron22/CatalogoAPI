using CatalagoAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace CatalagoAPI.DTOs
{
    public class CategoriaDTO
    {

        public int CategoriaId { get; set; }
        public string? Nome { get; set; }
        public string? ImageUrl { get; set; }
        public ICollection<ProdutoDTO>? Produtos { get; set; }
    }
}
