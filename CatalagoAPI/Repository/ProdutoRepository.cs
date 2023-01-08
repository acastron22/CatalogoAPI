using CatalagoAPI.Context;
using CatalagoAPI.Models;
using CatalagoAPI.Pagination;
using Microsoft.EntityFrameworkCore;

namespace CatalagoAPI.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext contexto) : base(contexto)
        {
        }

        public async Task<PagedList<Produto>> GetProdutos(ProdutosParameters produtosParameters)
        {
            //return Get()
            //    .OrderBy(on => on.Name)
            //    .Skip((produtosParameters.PageNumber - 1) * produtosParameters.PageSize)
            //    .Take(produtosParameters.PageSize)
            //    .ToList();
            return await PagedList<Produto>.ToPagedList(Get().OrderBy(on => on.ProdutoID), produtosParameters.PageNumber, produtosParameters.PageSize);
        }

        public async  Task<IEnumerable<Produto>> GetProdutosPorPreco()
        {
            return  await Get().OrderBy(c => c.Preco).ToListAsync();
        }
    }
}
