using APICatalogo.Models;

namespace APICatalogo.Repositories.Interfaces;

public interface IProdutosRepository : IRepository<Produto>
{
    IEnumerable<Produto> GetProdutoPorCategoria(int id);

    //IQueryable<Produto> GetProdutos();
    //Produto GetProduto(int id);
    //Produto Create(Produto produto);
    //bool Update(Produto produto);
    //bool Delete(int id);
}
