using AutoMapper;
using CatalagoAPI.Context;
using CatalagoAPI.DTOs;
using CatalagoAPI.Models;
using CatalagoAPI.Pagination;
using CatalagoAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CatalagoAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;

    public ProdutosController(IUnitOfWork context, IMapper mapper)
    {
        _uof = context;
        _mapper = mapper;
    }

    [HttpGet("menorpreco")]
    public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutosPreco()
    {
        var produtos =await _uof.ProdutoRepository.GetProdutosPorPreco();
        // Mapeamos produtos para uma lista de ProdutoDTO
        var produtosDto = _mapper.Map<List<ProdutoDTO>>(produtos);

        return produtosDto;
    }

    [HttpGet]
    public  ActionResult<IEnumerable<ProdutoDTO>> Get([FromQuery] ProdutosParameters produtosParameter)
    {
        var produtos =  _uof.ProdutoRepository.GetProdutos(produtosParameter);

        var metadata = new
        {
            produtos.TotalCount,
            produtos.PageSize,
            produtos.CurrentPage,
            produtos.TotalPages,
            produtos.HasNext,
            produtos.HasPrevius
        };

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));


        var produtosDTO = _mapper.Map<List<ProdutoDTO>>(produtos);
        return produtosDTO;
    }

    // produtos/id
    [HttpGet("{id}")]
    public async Task<ActionResult<ProdutoDTO>> Get(int id) // o BindRequired torna o campo obrigatório
    {
        var produto = await _uof.ProdutoRepository.GetById(p => p.ProdutoID == id);
        if (produto is null)
        {
            return NotFound("Produto não encontrando");
        }

        // não estou retornando uma lista de produto, e sim só um produto,por isso não uso a classe List
        var produtoDTO = _mapper.Map<ProdutoDTO>(produto);

        return produtoDTO;
    }

    // /produtos - mesmo tendo um atributo diferente, o método só atende o request para post
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ProdutoDTO produtoDto)
    {
        var produto = _mapper.Map<Produto>(produtoDto);
        _uof.ProdutoRepository.Add(produto);
       await _uof.Commit();

        var produtoDTO = _mapper.Map<ProdutoDTO>(produto);

        return new CreatedAtRouteResult("ObterProduto",
            new { id = produto.ProdutoID }, produtoDTO);
    }

    // /produtos/id
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, Produto produtoDto)
    {
        if (id != produtoDto.ProdutoID)
        {
            return BadRequest();
        }

        var produto = _mapper.Map<Produto>(produtoDto);

        _uof.ProdutoRepository.Update(produto);
        await _uof.Commit();

        return Ok(produto);
    }

    // /produtos/id
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ProdutoDTO>>Delete(int id)
    {
        var produto = await _uof.ProdutoRepository.GetById(p => p.ProdutoID == id);
        if (produto is null)
        {
            return NotFound("Produto não localizado...");
        }


        _uof.ProdutoRepository.Delete(produto);
        await _uof.Commit();

        var produtoDto = _mapper.Map<ProdutoDTO>(produto);

        return Ok(produtoDto);
    }
}
