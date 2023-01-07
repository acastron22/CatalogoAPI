using CatalagoAPI.Models;
using CatalagoAPI.Pagination;

namespace CatalagoAPI.Repository
{
    public interface ICategoriaRepository: IRepository<Categoria>
    {

        PagedList<Categoria>
            GetCategorias(CategoriasParameters categoriaParameters);
        IEnumerable<Categoria> GetCategoriasProdutos();
    }
}
