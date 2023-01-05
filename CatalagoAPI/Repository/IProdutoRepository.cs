using CatalagoAPI.Models;

namespace CatalagoAPI.Repository
{
    public interface IProdutoRepository:IRepository<Produto>
    {
        IEnumerable<Produto> GetProdutosPorPreco();
    }
}
