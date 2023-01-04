using CatalagoAPI.Context;
using CatalagoAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace CatalagoAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProdutosController(AppDbContext context)
    {
        _context = context;
    }

    // /produtos
    //[HttpGet("primeiro")] // este endpoit agora é um get igual ao get de receber todos os produtos
    //// o endpoint e o verbo são os mesmos
    //// isso gera um erro e não permite carregar a aplicação porque há mais de um endpoint atendendo esse request
    //// para resolver esse problema, adicionamos um parâmetro dps do atributo como o exemplo '("primeiro")'
    //// primeiro vai compor a rota e agora o endpoint será /produtos/primeiro
    //public ActionResult<Produto> GetPrimeiro()
    //{
    //    var produtos = _context.Produtos?.FirstOrDefault();
    //    if (produtos is null)
    //    {
    //        return NotFound("Produtos não encontrados");
    //    }
    //    return produtos;
    //}

    // Requisições async

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Produto>>> Get2()
    {
        return await _context.Produtos.AsNoTracking().ToListAsync();
    }

    //[HttpGet("{valor:alpha:length(5)}")]
    //public ActionResult<Produto> Get2(string valor)
    //{

    //    return _context.Produtos.FirstOrDefault();
    //}

    // /produtos
    //[HttpGet]
    //public ActionResult<IEnumerable<Produto>> Get()
    //{
    //    var produtos = _context.Produtos?.ToList();
    //    if (produtos is null)
    //    {
    //        return NotFound("Produtos não encontrados");
    //    }
    //    return produtos;
    //}

    // produtos/id
    [HttpGet("{id}", Name = "ObterProduto")]
    public async Task<ActionResult<Produto>> Get(int id, [BindRequired] string nome) // o BindRequired torna o campo obrigatório
    {
        var nomeProduto = nome;
        var produto = await _context.Produtos?.FirstOrDefaultAsync(p => p.ProdutoID == id);
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
        _context.Produtos?.Add(produto);
        _context.SaveChanges();

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

        _context.Entry(produto).State = EntityState.Modified;
        _context.SaveChanges();

        return Ok(produto);
    }

    // /produtos/id
    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var produto = _context.Produtos?.FirstOrDefault(p => p.ProdutoID == id);
        if (produto is null)
        {
            return NotFound("Produto não localizado...");
        }
        _context.Produtos?.Remove(produto);
        _context.SaveChanges();

        return Ok(produto);
    }
}
