using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalagoAPI.Migrations
{
    public partial class PopulaProdutos : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Produtos (Name, Descricao, Preco, ImageUrl, Estoque, DataCadastro, categoriaId)" +
                   "Values('Coca-Cola Zero', 'Refrigerante de Cola 350ml zero açúcar', 5.45,'cocacolazero.jpg',50,now(), 1)");

            mb.Sql("Insert into Produtos (Name, Descricao, Preco, ImageUrl, Estoque, DataCadastro, categoriaId)" +
                   "Values('Lanche de Atum', 'Lanche de Atum com maionese', 8.50,'lancheatum.jpg',50,now(), 2)");

            mb.Sql("Insert into Produtos(Name,Descricao,Preco,ImageUrl,Estoque,DataCadastro,CategoriaId)" +
                    "Values('Pudim 100 g','Pudim de leite condensado 100g',6.75,'pudim.jpg',20,now(),3)");
        }

        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Produtos");
        }
    }
}
