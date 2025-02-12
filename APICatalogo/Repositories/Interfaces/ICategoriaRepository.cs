using APICatalogo.Models;
using APICatalogo.Pagination;

namespace APICatalogo.Repositories.Interfaces;

public interface ICategoriaRepository : IRepository<Categoria>
{
    PagedList<Categoria> GetCategorias(CategoriasParameter categoriasParameter);
    PagedList<Categoria> GetCategoriasFiltroNome(CategoriasFiltroNome categoriasFiltroNome);

    //IEnumerable<Categoria> GetCategorias();
    //Categoria GetCategoria(int id);
    //Categoria Create(Categoria categoria);
    //Categoria Update(Categoria categoria);
    //Categoria Delete(int id);
}
