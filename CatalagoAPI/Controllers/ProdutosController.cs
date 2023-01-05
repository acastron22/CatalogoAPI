using CatalagoAPI.Context;
using CatalagoAPI.Models;
using CatalagoAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace CatalagoAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IUnitOfWork _uof;

    public ProdutosController(IUnitOfWork context)
    {
        _uof = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Produto>> GetProdutosPreco()
    {
        return _uof.ProdutoRepository.GetProdutosPorPreco().ToList();
    }

    [HttpGet]
    public  ActionResult<IEnumerable<Produto>> Get()
    {
        return  _uof.ProdutoRepository.Get().ToList();
    }

    // produtos/id
    [HttpGet("{id}", Name = "ObterProduto")]
    public ActionResult<Produto> Get(int id, [BindRequired] string nome) // o BindRequired torna o campo obrigatório
    {
        var nomeProduto = nome;
        var produto = _uof.ProdutoRepository.GetById(p => p.ProdutoID == id);
        if (produto is null)
        {
            return NotFound("Produto não encontrando");
        }
        return produto;
    }

    // /produtos - mesmo tendo um atributo diferente, o método só atende o request para post
    [HttpPost]
    public ActionResult Post(Produto produto)
    {
        if (produto is null)
        {
            return BadRequest();
        }
        _uof.ProdutoRepository.Add(produto);
        _uof.Commit();

        return new CreatedAtRouteResult("ObterProduto",
            new { id = produto.ProdutoID }, produto);
    }

    // /produtos/id
    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Produto produto)
    {
        if (id != produto.ProdutoID)
        {
            return BadRequest();
        }

        _uof.ProdutoRepository.Update(produto);
        _uof.Commit();

        return Ok(produto);
    }

    // /produtos/id
    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var produto = _uof.ProdutoRepository.GetById(p => p.ProdutoID == id);
        if (produto is null)
        {
            return NotFound("Produto não localizado...");
        }
        _uof.ProdutoRepository.Delete(produto);
        _uof.Commit();

        return Ok(produto);
    }
}
