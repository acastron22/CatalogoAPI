# Model Biding
	- é um recurso que permite mapear dados de uma requisição HTTP para os parâmetros de uma Action de um controlador

	Esse mapeamento inclui todos os tipos de parâmetros: números, string, array, lista, tipos complexos, lista de objetos, etc.

	Quando a API recebe uma requisição HTTp, ela a roteia para um método Action específico de um controlador com base na rota definida.
	Ela determina qual o método Action será executado com base no que está definido na rota e, em seguida, vincula os valores da requisição HTTP aos parâmetros desse método Action

	[HttpGet("{id}")]
	public async Task<ActionResult<Produto>>Get(int id){
		...
	}
	acima o template de rota id vai extrair o valor da URL e vai passar para o método Action com parâmetro id do tipo int.

	Fontes de dados:
	1 - Valores de formulários: (POST e PUT) - Enviandos no corpo do request

	2 - Rotas: [Route("api/[Controller]")] ou HttpGet("{id}")

	3 - Query String : api/produtos/4?nome=Suco&ativo=true

## Query Strings
	- o valor é passado nas URLS e a query string inicia com o valor de interrogação (?)
		-https://localhost:XXXXXX/api/produtos/4?nome=Suco&ativo=true
		QueryString => 'nome=Suco&ativo=true'.
			=> para separar os parâmetros, utiliza o &. No exemplo acima temos dois parâmetros, nome e ativo.

	é maperado pelo método action

	[Httpget({id})]
	public ActionResult<Produto>Get(int id, string nome, bool ativo = false){
		... 
	}
## Formulário

	[HttpPost]{
		public ActionResult Post([FromBody]Produto produto){ // os dados virão do corpo da requisição
			...
		}
	}

	[HttpPut]{
		public ActionResult Put(int id, [FromBody] Produto produto){ // os dados virão do corpo da requisição
			...
		}
	}

	Model Binding permite uma configuração usando atributos para indicar a fonte dos dados ou mesmo se desejamos que ele ocorra ou não

## Atributos para definir se o Model Binding vai ocorrer ou não

	1 - BindRequired - Este atributo adiciona um erro ao ModelState se a vinculação de dados aos parâmetros não puder ocorrer.

	[HttpGet("{id}")]

	public async Task<ActionResult<Produto>>Get(int id, [BindRequired] string nome){
		..
	}

	exemplo visto no código


	2 - BindNever
	Informa ao model binder para não vincular a informação ao parâmetro

	public class Categoria
	{
		public int CategoriaId {get;set;}
		public string Nome {get;set}

		[BindNever]
		public string ImageUrl {get;set;}
	}

# Atributo FromService

	O Atributo [FromServices] permite injetar as dependências diretamente no método Action do controlador que requer a dependência.
	Para fazer isso funcionar basta adicionar o atributo ao método Action desejado, e a ASP .NET Core vai buscar de forma automática nas dependências
	as implementações e vai fazer injeção da dependência.

	- Definir os serviços
	- Registrar os serviços
	- Aplicar o atributo ao método Action do Controlador que requer o serviço