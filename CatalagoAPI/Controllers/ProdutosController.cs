using AutoMapper;
using CatalagoAPI.Context;
using CatalagoAPI.DTOs;
using CatalagoAPI.Models;
using CatalagoAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

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
    public ActionResult<IEnumerable<ProdutoDTO>> GetProdutosPreco()
    {
        var produtos = _uof.ProdutoRepository.GetProdutosPorPreco().ToList();
        // Mapeamos produtos para uma lista de ProdutoDTO
        var produtosDto = _mapper.Map<List<ProdutoDTO>>(produtos);

        return produtosDto;
    }

    [HttpGet]
    public  ActionResult<IEnumerable<ProdutoDTO>> Get()
    {
        var produtos =  _uof.ProdutoRepository.Get().ToList();

        var produtosDTO = _mapper.Map<List<ProdutoDTO>>(produtos);
        return produtosDTO;
    }

    // produtos/id
    [HttpGet("{id}")]
    public ActionResult<ProdutoDTO> Get(int id) // o BindRequired torna o campo obrigatório
    {
        var produto = _uof.ProdutoRepository.GetById(p => p.ProdutoID == id);
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
    public ActionResult Post([FromBody] ProdutoDTO produtoDto)
    {
        var produto = _mapper.Map<Produto>(produtoDto);
        _uof.ProdutoRepository.Add(produto);
        _uof.Commit();

        var produtoDTO = _mapper.Map<ProdutoDTO>(produto);

        return new CreatedAtRouteResult("ObterProduto",
            new { id = produto.ProdutoID }, produtoDTO);
    }

    // /produtos/id
    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Produto produtoDto)
    {
        if (id != produtoDto.ProdutoID)
        {
            return BadRequest();
        }

        var produto = _mapper.Map<Produto>(produtoDto);

        _uof.ProdutoRepository.Update(produto);
        _uof.Commit();

        return Ok(produto);
    }

    // /produtos/id
    [HttpDelete("{id:int}")]
    public ActionResult<ProdutoDTO>Delete(int id)
    {
        var produto = _uof.ProdutoRepository.GetById(p => p.ProdutoID == id);
        if (produto is null)
        {
            return NotFound("Produto não localizado...");
        }


        _uof.ProdutoRepository.Delete(produto);
        _uof.Commit();

        var produtoDto = _mapper.Map<ProdutoDTO>(produto);

        return Ok(produtoDto);
    }
}
