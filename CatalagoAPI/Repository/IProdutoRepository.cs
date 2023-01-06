using CatalagoAPI.Models;
using CatalagoAPI.Pagination;
using System.Collections.Generic;

namespace CatalagoAPI.Repository
{
    public interface IProdutoRepository:IRepository<Produto>
    {
        PagedList<Produto> GetProdutos(ProdutosParameters produtosParameters);
        IEnumerable<Produto> GetProdutosPorPreco();
    }
}
