using CatalagoAPI.Models;
using CatalagoAPI.Pagination;

namespace CatalagoAPI.Repository
{
    public interface ICategoriaRepository: IRepository<Categoria>
    {

        Task<PagedList<Categoria>>
            GetCategorias(CategoriasParameters categoriaParameters);
        Task<IEnumerable<Categoria>> GetCategoriasProdutos();
    }
}
