using APICatalogo.Models;
using APICatalogo.Pagination;

namespace APICatalogo.Repositories.Interfaces;

public interface IProdutosRepository : IRepository<Produto>
{
    //IEnumerable<Produto> GetProdutos(ProdutosParameter produtosParameter);
    PagedList<Produto> GetProdutos(ProdutosParameter produtosParameter);
    PagedList<Produto> GetProdutosFiltroPreco(ProdutosFiltroPreco produtosFiltroPreco);
    IEnumerable<Produto> GetProdutoPorCategoria(int id);

    //IQueryable<Produto> GetProdutos();
    //Produto GetProduto(int id);
    //Produto Create(Produto produto);
    //bool Update(Produto produto);
    //bool Delete(int id);
}
