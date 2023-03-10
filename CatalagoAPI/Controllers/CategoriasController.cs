using AutoMapper;
using CatalagoAPI.Context;
using CatalagoAPI.DTOs;
using CatalagoAPI.Models;
using CatalagoAPI.Pagination;
using CatalagoAPI.Repository;
using CatalagoAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson.IO;
using System.Text.Json;

namespace CatalagoAPI.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
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
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategoriasProdutos()
    {
        _logger.LogInformation("======================= GET categorias/categorias =======================");
        var categorias = await _uof.CategoriaRepository.GetCategoriasProdutos();
        var categoriasDto = _mapper.Map<List<CategoriaDTO>>(categorias);
        return categoriasDto;
    }

    // retorna as  categorias
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>>
        Get([FromQuery] CategoriasParameters categoriasParameters)
    {
        var categorias = await _uof.CategoriaRepository.
                           GetCategorias(categoriasParameters);
        var metadata = new
        {
            categorias.TotalCount,
            categorias.PageSize,
            categorias.CurrentPage,
            categorias.TotalPages,
            categorias.HasNext,
            categorias.HasPrevius
        };

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

        var categoriasDto = _mapper.Map<List<CategoriaDTO>>(categorias);
        return categoriasDto;

    }

    // retorna uma categoria por id
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoriaDTO>> Get(int id)
    {
            _logger.LogInformation($"======================= GET categorias/categorias/id = {id} =======================");
            var categoria = await _uof.CategoriaRepository.GetById(p=>p.CategoriaId == id);
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
    public async Task<ActionResult> Post([FromBody]CategoriaDTO categoriaDto)
    {

        var categoria = _mapper.Map<Categoria>(categoriaDto);
        _uof.CategoriaRepository.Add(categoria);
       await _uof.Commit();

        var categoriaDTO = _mapper.Map<CategoriaDTO>(categoria);

        return new CreatedAtRouteResult("ObterCategoria",
            new { id = categoria.CategoriaId }, categoriaDTO);
    }


    //atualiza uma categoria passando seu Id
    [HttpPut("{id:int}")]
    public  async Task<ActionResult> Put(int id, CategoriaDTO categoriaDto)
    {
        if (id != categoriaDto.CategoriaId)
        {
            return BadRequest();
        }

        var categoria = _mapper.Map<Categoria>(categoriaDto);

        _uof.CategoriaRepository.Update(categoria);
       await  _uof.Commit();

        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<CategoriaDTO>> Delete(int id)
    {
        var categoria =  await _uof.CategoriaRepository.GetById(p => p.CategoriaId == id);

        if (categoria is null)
        {
            return NotFound("Categoria não localizado.");
        }
        _uof.CategoriaRepository.Delete(categoria);
       await _uof.Commit();

        var categoriaDto = _mapper.Map<CategoriaDTO>(categoria);
        return categoriaDto;
    }
}
