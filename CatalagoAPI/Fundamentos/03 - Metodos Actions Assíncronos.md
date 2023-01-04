# Métodos Actions Assíncronos
	- Até o momento, métodos criados nos controladores são sincronos

#	Método síncrono:
		- o servidor web de uma aplicaão .NET possui um pool de threads que são usadas para servir as requisições que chegam em um método action

		- Quando uma reques chega, uma thread desse pool é encarregada de processar esse request
		- Se o request for processado de forma síncrona, a thred encarregada ficará bloqueada até o request finalizar
		- Assim, execução dos métodos é contínua, se uma thread começar a executar um método, ela irá ficar ocupada até a execução do método terminar.

	1 thread -> |<---A--->| |<----B------------------->||<------C-------->|
	cada etapa é feita somente dps q a outra termina


#	Método Assíncrono:
		- o servidor web de uma aplicaão .NET possui um pool de threads que são usadas para servir as requisições que chegam em um método action
		- Quando uma reques chega, uma thread desse pool é encarregada de processar esse request

		- Se o request for processado de forma assíncrona, a thred é devikvuda ao pool para servir novos requests.

		- Assim, a execução dos métodos não é contínua.
		- Quando a operação assíncrona terminar, a thread é avisada e retoma o controle de execução da Action.


## Programação Assíncrona
	- a plataforma .NET oferece uma abordagem simplificada à programação assincrona fazendo uso das palavras chaves async e await para criar métodos 
	assíncronos (Task-based Asynchonus Pattern - TAP)

	- As palavras chaves async e await e o tipo task são o coração da programação assíncrona no .NET.

	[HttpGet]
	public async Task<Action Result<IEnumerable<Categoria>>> GetCategoriasAsync()
	{
		return await _context.Categorias.ToListAsync();
	}

	Essa action retorna um objeto do tipo Task<ActionResult<IEnumerable<T>>>; faz uso dos modificadores async e await e seu nome termina com Async.