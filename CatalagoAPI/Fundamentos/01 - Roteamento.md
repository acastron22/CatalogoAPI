# # Roteamento - WebAPIs

o roteamento e representado pelo nome que vem antes do controlle, por exemplo

"
    Retirado da pasta controller, a minha rota será /produtos, porque produtos é o nome que bem antes de controller 

    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        
    "

# O que é o roteamento
    - Mapeia as requisições feitas via URI
    - Despacha as requisições para os endpoints
    - Pode extrair valores da URL da requisição
    - Pode gerar URLs que mapeiam para os endpoints


# Roteamento - Configurar

    - As aplicações podem configurar o roteamento pelos 
        - controllers*, 
        - Razor Pages, 
        - SignalR, 
        - Middleware* (habilitado para o endpoint) e 
        - Delegates* e Lambdas* registrados no roteamento

# Roteramento nas Web APIs é baseado em atributos
    [Route("[Controller]")]

    a rota é determinada com base nos atributos definidos nos controladores e métodos action

    os atributos são HttpGet, HttpPost, HttpPut e HttpDelete


# Padrões de rotearmonto

utilizamos o controlador Route para ter uma rota padrão.
[Route(´"[controller]")] => /produtos, /categorias
[Route(´"api/[controller]")] => /api/produtos, /api/categorias

 - Combinar o template da rota com a rota definida no atributo Route
 [HttpGet("primeiro")] => /produtos/primeiro, /categorias/primeiro
  foi o q fizemos em ProdutosController

 [HttpGet("{id}")] => /produtos/{id}, /categorias/{id}


 mas posso tbm ignorar a rota definida pelo atributo Route colocando uma barra apos o atributo
 [HttpGet("/primeiro")] => /primeiro

 posso passar também um segundo parametro
 [HttpGet("{id}/{outroParametro}")]

 [HttpGet("{id:int}/{param2}", Name = "ObterProduto")]
    public ActionResult<Produto> Get(int id, string param2)
    {
        var parametro = param2;
        var produto = _context.Produtos?.FirstOrDefault(p => p.ProdutoID == id);
        if (produto is null)
        {
            return NotFound("Produto não encontrando");
        }
        return produto;
    }

## Restrição de Rotas
    - ajudam a filtrar ou impedir que os valores indesejados atinjam os métodos Action do controlador
    - isso é feito verificando a restrição com relação ao valor do parâmetro da URL

    Por exemplo, e se for passado o valor 0, ou negativo no id em api/produtos/id? 

    Resposta feita com códigos em produtosController