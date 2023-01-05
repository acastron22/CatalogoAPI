using CatalagoAPI.Models;

namespace CatalagoAPI.Repository
{
    public interface ICategoriaRepository: IRepository<Categoria>
    {
        IEnumerable<Categoria> GetCategoriasProdutos();
    }
}
