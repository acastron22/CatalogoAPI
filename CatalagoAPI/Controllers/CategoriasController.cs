using CatalagoAPI.Context;
using CatalagoAPI.Models;
using CatalagoAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalagoAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly AppDbContext _context;

    private readonly ILogger _logger;

    public CategoriasController(AppDbContext context, ILogger<CategoriasController> logger)
    {
        _context = context;
        _logger = logger;
    }


    [HttpGet("saudacao/{nome}")]
    public ActionResult<string> GetSaudacao([FromServices] IMeuServico meuServico, string nome)
    {
        return meuServico.Saudacao(nome);
    }
        

    [HttpGet("produtos")]
    public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
    {
        _logger.LogInformation("======================= GET categorias/categorias =======================");
        return _context.Categorias.Include( p => p.Produtos).AsNoTracking().ToList();
    }


    [HttpGet]
    public ActionResult<IEnumerable<Categoria>> Get()
    {
        try
        {
          //  throw new DataMisalignedException();
            return _context.Categorias.AsNoTracking().ToList();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
                    
    }


    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public ActionResult<Categoria> Get(int id)
    {
        try
        {
            _logger.LogInformation($"======================= GET categorias/categorias/id = {id} =======================");
            var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);
            if (categoria is null)
            {
                _logger.LogInformation($"======================= GET categorias/categorias/id = {id} NOT FOUND =======================");
                return NotFound($"Categoria com id = {id} não encontrada");
            }
            return Ok(categoria);
        }
        catch (Exception)
        {

            throw;
        }
        
    }

    [HttpPost]
    public ActionResult Post(Categoria categoria)
    {
        if (categoria is null)
        {
            return BadRequest();
        }
        _context.Categorias.Add(categoria);
        _context.SaveChanges();

        return new CreatedAtRouteResult("ObterCategoria",
            new { id = categoria.CategoriaId }, categoria);
    }


    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Categoria categoria)
    {
        if (id != categoria.CategoriaId)
        {
            return BadRequest();
        }

        _context.Entry(categoria).State = EntityState.Modified;
        _context.SaveChanges();

        return Ok(categoria);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);
        if (categoria is null)
        {
            return NotFound("Categoria não localizado.");
        }
        _context.Categorias.Remove(categoria);
        _context.SaveChanges();

        return Ok(categoria);
    }
}
