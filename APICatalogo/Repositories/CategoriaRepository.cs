using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbContext context) : base(context)
    {

    }

    public PagedList<Categoria> GetCategorias(CategoriasParameter categoriasParameter)
    {
        var categorias = GetAll().OrderBy(p => p.CategoriaId).AsQueryable().AsQueryable();
        var categoriasOrdenados = PagedList<Categoria>.ToPagedList(categorias, 
            categoriasParameter.PageNumber, categoriasParameter.PageSize);
        return categoriasOrdenados;
    }

    public PagedList<Categoria> GetCategoriasFiltroNome(CategoriasFiltroNome categoriasFiltroNome)
    {
        var produtos = GetAll().AsQueryable();

        if (!string.IsNullOrEmpty(categoriasFiltroNome.Nome))
        {
            produtos = produtos.Where(p => p.Nome.Contains(categoriasFiltroNome.Nome)).OrderBy(p => p.Nome);
        }

        var produtosFiltrados = PagedList<Categoria>.ToPagedList(produtos,
                                                                              categoriasFiltroNome.PageNumber,
                                                                              categoriasFiltroNome.PageSize);
        return produtosFiltrados;
    }



    //private readonly AppDbContext _context;

    //public CategoriaRepository(AppDbContext context)
    //{
    //    _context = context;
    //}

    //public IEnumerable<Categoria> GetCategorias()
    //{
    //    return _context.Categorias.ToList();
    //}

    //public Categoria GetCategoria(int id)
    //{
    //    return _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);
    //}

    //public Categoria Create(Categoria categoria)
    //{
    //    if (categoria is null)
    //        throw new ArgumentNullException(nameof(categoria));

    //    _context.Categorias.Add(categoria);
    //    _context.SaveChanges();

    //    return categoria;
    //}
    //public Categoria Update(Categoria categoria)
    //{
    //    if (categoria is null)
    //        throw new ArgumentNullException(nameof(categoria));

    //    _context.Entry(categoria).State = EntityState.Modified;
    //    _context.SaveChanges();

    //    return categoria;
    //}

    //public Categoria Delete(int id)
    //{
    //    var categoria = _context.Categorias.Find(id);

    //    if (categoria is null)
    //        throw new ArgumentNullException(nameof(categoria));

    //    _context.Categorias.Remove(categoria);
    //    _context.SaveChanges();

    //    return categoria;
    //}

}
