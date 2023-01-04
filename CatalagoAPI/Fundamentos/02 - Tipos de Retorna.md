# Métodos Action: Tipos de Retorno

	- Tipo Específico
	- IActionResult
	- ActionResult<T>

## Tipo específico
	- Retorna um tipo de dados complexo ou primitivo (sring, int, etc)

	[HttpGet]
	public string Get()
	{
		return "Retornando uma string"
	}

	
	[HttpGet]
	public Produto Get()
	{
		var produto = _context.Produtos.FirstOrDefault();
		return produto;
		// retorna um tipo Produto
	}

## Tipo IActionResult
	
	- O tipo de retorno IActionResult é apropriado quando vários tipos de retorno ActionResult são possíveis em método Action.

	[HttpGet]
	public IActionResult Get()
	{
		var produto = _context.Produtos.FirstOrDefault();
		if(produto == null)
		{
			return NotFound();
		}
		return Ok(produto);
	}

	A classe abstrata ActionResult implementa a interface IActionResult

## Tipo ActionResult<T>

	- Permite o retorno de um tipo derivado de ActionResult ou o retorno de um tipo específico(T).
		[HttpGet]
	public ActionResult<Produto> Get()
	{
		var produto = _context.Produtos.FirstOrDefault();
		if(produto == null)
		{
			return NotFound();
		}
		return produto;
	}
