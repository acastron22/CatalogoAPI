using CatalagoAPI.Context;
using CatalagoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalagoAPI.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(AppDbContext contexto) : base(contexto)
        {

        }
        public IEnumerable<Categoria> GetCategoriasProdutos()
        {
            return Get().Include(x => x.Produtos);
        }
    }
}
