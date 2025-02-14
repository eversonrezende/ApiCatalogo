using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repositories.Interfaces;

namespace APICatalogo.Repositories;

public class ProdutoRepository : Repository<Produto>, IProdutosRepository
{
    public ProdutoRepository(AppDbContext contexto) : base(contexto)
    {

    }

    public async Task<IEnumerable<Produto>> GetProdutoPorCategoriaAsync(int id)
    {
        var produtos = await GetAllAsync();

        return produtos.Where(c => c.CategoriaId == id);
    }

    public async Task<PagedList<Produto>> GetProdutosAsync(ProdutosParameter produtosParameter)
    {
        var produtos = await GetAllAsync();

        var produtosOrdenados = produtos.OrderBy(p => p.ProdutoId).AsQueryable();

        var produtosPaginados = PagedList<Produto>.ToPagedList(produtosOrdenados, produtosParameter.PageNumber, produtosParameter.PageSize);

        return produtosPaginados;
    }

    public async Task<PagedList<Produto>> GetProdutosFiltroPrecoAsync(ProdutosFiltroPreco produtosFiltroPreco)
    {
        var produtos = await GetAllAsync();

        if (produtosFiltroPreco.Preco.HasValue && !string.IsNullOrEmpty(produtosFiltroPreco.PrecoCriterio))
        {
            if (produtosFiltroPreco.PrecoCriterio.Equals("maior", StringComparison.OrdinalIgnoreCase))
            {
                produtos = produtos.Where(p => p.Preco > produtosFiltroPreco.Preco.Value).OrderBy(p => p.Preco);
            }
            else if (produtosFiltroPreco.PrecoCriterio.Equals("menor", StringComparison.OrdinalIgnoreCase))
            {
                produtos = produtos.Where(p => p.Preco < produtosFiltroPreco.Preco.Value).OrderBy(p => p.Preco);
            }
            else if (produtosFiltroPreco.PrecoCriterio.Equals("igual", StringComparison.OrdinalIgnoreCase))
            {
                produtos = produtos.Where(p => p.Preco == produtosFiltroPreco.Preco.Value).OrderBy(p => p.Preco);
            }
        }

        var produtosFiltrados = PagedList<Produto>.ToPagedList(produtos.AsQueryable(),
                                                                              produtosFiltroPreco.PageNumber,
                                                                              produtosFiltroPreco.PageSize);

        return produtosFiltrados;
    }

}
