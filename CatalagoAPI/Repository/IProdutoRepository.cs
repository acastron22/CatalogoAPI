using CatalagoAPI.Models;
using CatalagoAPI.Pagination;
using System.Collections.Generic;

namespace CatalagoAPI.Repository
{
    public interface IProdutoRepository:IRepository<Produto>
    {
        Task<PagedList<Produto>> GetProdutos(ProdutosParameters produtosParameters);
       Task<IEnumerable<Produto>> GetProdutosPorPreco();
    }
}
