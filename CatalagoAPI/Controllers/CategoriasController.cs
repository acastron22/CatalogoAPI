using AutoMapper;
using CatalagoAPI.Context;
using CatalagoAPI.DTOs;
using CatalagoAPI.Models;
using CatalagoAPI.Repository;
using CatalagoAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalagoAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly IUnitOfWork _uof;

    private readonly ILogger _logger;

    private readonly IMapper _mapper;

    public CategoriasController(IUnitOfWork context, ILogger<CategoriasController> logger,
        IMapper mapper)
    {
        _uof = context;
        _logger = logger;
        _mapper = mapper;
    }
        
    // retorna todas as categorias e os produtos dentro dessa categoria
    [HttpGet("produtos")]
    public ActionResult<IEnumerable<CategoriaDTO>> GetCategoriasProdutos()
    {
        _logger.LogInformation("======================= GET categorias/categorias =======================");
        var categorias = _uof.CategoriaRepository.GetCategoriasProdutos().ToList();
        var categoriasDto = _mapper.Map<List<CategoriaDTO>>(categorias);
        return categoriasDto;
    }

    // retorna as 
    [HttpGet]
    public ActionResult<IEnumerable<CategoriaDTO>> Get()
    {
        try
        {
          //  throw new DataMisalignedException();
            var categorias = _uof.CategoriaRepository.Get().ToList();
            var categoriasDto = _mapper.Map<List<CategoriaDTO>>(categorias);
            return categoriasDto;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
        }
                    
    }

    // retorna uma categoria por id
    [HttpGet("{id:int}")]
    public ActionResult<CategoriaDTO> Get(int id)
    {
            _logger.LogInformation($"======================= GET categorias/categorias/id = {id} =======================");
            var categoria = _uof.CategoriaRepository.GetById(p=>p.CategoriaId == id);
            if (categoria is null)
            {
                _logger.LogInformation($"======================= GET categorias/categorias/id = {id} NOT FOUND =======================");
                return NotFound($"Categoria com id = {id} não encontrada");
            }

            var categoriaDto = _mapper.Map<CategoriaDTO>(categoria);
            return categoriaDto;        
    }

    // publica uma nova categoria
    [HttpPost]
    public ActionResult Post([FromBody]CategoriaDTO categoriaDto)
    {

        var categoria = _mapper.Map<Categoria>(categoriaDto);
        _uof.CategoriaRepository.Add(categoria);
        _uof.Commit();

        var categoriaDTO = _mapper.Map<CategoriaDTO>(categoria);

        return new CreatedAtRouteResult("ObterCategoria",
            new { id = categoria.CategoriaId }, categoriaDTO);
    }


    //atualiza uma categoria passando seu Id
    [HttpPut("{id:int}")]
    public ActionResult Put(int id, CategoriaDTO categoriaDto)
    {
        if (id != categoriaDto.CategoriaId)
        {
            return BadRequest();
        }

        var categoria = _mapper.Map<Categoria>(categoriaDto);

        _uof.CategoriaRepository.Update(categoria);
        _uof.Commit();

        return Ok();
    }

    [HttpDelete("{id:int}")]
    public ActionResult<CategoriaDTO> Delete(int id)
    {
        var categoria = _uof.CategoriaRepository.GetById(p => p.CategoriaId == id);

        if (categoria is null)
        {
            return NotFound("Categoria não localizado.");
        }
        _uof.CategoriaRepository.Delete(categoria);
        _uof.Commit();

        var categoriaDto = _mapper.Map<CategoriaDTO>(categoria);
        return categoriaDto;
    }
}
