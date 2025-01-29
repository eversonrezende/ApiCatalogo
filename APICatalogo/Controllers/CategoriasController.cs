using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly IUnitOfWork _uof;
    private readonly ILogger<CategoriasController> _logger;

    public CategoriasController(IUnitOfWork uof, ILogger<CategoriasController> logger)
    {
        _uof = uof;
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CategoriaDTO>> Get()
    {
        var categorias = _uof.CategoriaRepository.GetAll();
        var categoriasDto = new List<CategoriaDTO>();

        if (categorias is null)
        {
            _logger.LogWarning($"Categorias não encontradas..");
            return NotFound("Categorias não encontradas...");
        }

        foreach (var categoria in categorias)
        {
            var categoriaDto = new CategoriaDTO
            {
                CategoriaId = categoria.CategoriaId,
                Nome = categoria.Nome,
                ImagemUrl = categoria.ImagemUrl
            };

            categoriasDto.Add(categoriaDto);
        }

        return Ok(categoriasDto);
    }

    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public ActionResult<CategoriaDTO> Get(int id)
    {
        var categoria = _uof.CategoriaRepository.Get(c => c.CategoriaId == id);

        if (categoria is null)
        {
            _logger.LogWarning($"Categoria com id = {id} não encontrada.");
            return NotFound("Categoria não encontrada...");
        }

        var categoriaDto = new CategoriaDTO
        {
            CategoriaId = categoria.CategoriaId,
            Nome = categoria.Nome,
            ImagemUrl = categoria.ImagemUrl
        };

        return Ok(categoriaDto);
    }

    [HttpPost]
    public ActionResult<CategoriaDTO> Post(CategoriaDTO categoriaDto)
    {
        if (categoriaDto is null)
        {
            _logger.LogWarning("Categoria nula...");
            return BadRequest();
        }

        var categoria = new Categoria
        {
            CategoriaId = categoriaDto.CategoriaId,
            Nome = categoriaDto.Nome,
            ImagemUrl = categoriaDto.ImagemUrl
        };

        var categoriaCriada = _uof.CategoriaRepository.Create(categoria);
        _uof.Commit();

        categoriaDto.CategoriaId = categoriaCriada.CategoriaId;
        categoriaDto.Nome = categoriaCriada.Nome;
        categoriaDto.ImagemUrl = categoriaCriada.ImagemUrl;

        return new CreatedAtRouteResult("ObterCategoria", new { id = categoriaDto.CategoriaId }, categoriaDto);
    }

    [HttpPut("{id:int}")]
    public ActionResult<CategoriaDTO> Put(int id, CategoriaDTO categoriaDto)
    {
        if (id != categoriaDto.CategoriaId)
        {
            _logger.LogWarning($"Categoria com id = {id} não corresponde ao id da categoria...");
            return BadRequest("Dados inválidos");
        }

        var categoria = new Categoria
        {
            CategoriaId = categoriaDto.CategoriaId,
            Nome = categoriaDto.Nome,
            ImagemUrl = categoriaDto.ImagemUrl
        };

        var categoriaAtualizada = _uof.CategoriaRepository.Update(categoria);
        _uof.Commit();

        categoriaDto.CategoriaId = categoriaAtualizada.CategoriaId;
        categoriaDto.Nome = categoriaAtualizada.Nome;
        categoriaDto.ImagemUrl = categoriaAtualizada.ImagemUrl;

        return Ok(categoriaDto);
    }

    [HttpDelete("{id:int}")]
    public ActionResult<CategoriaDTO> Delete(int id)
    {
        var categoria = _uof.CategoriaRepository.Get(c => c.CategoriaId == id);

        if (categoria is null)
        {
            _logger.LogWarning($"Categoria com id = {id} não encontrada...");
            return NotFound("Categoria não encontrada...");
        }

        var categoriaExcluida = _uof.CategoriaRepository.Delete(categoria);
        _uof.Commit();

        var categoriaDto = new CategoriaDTO
        {
            CategoriaId = categoriaExcluida.CategoriaId,
            Nome = categoriaExcluida.Nome,
            ImagemUrl = categoriaExcluida.ImagemUrl
        };

        return Ok(categoriaDto);
    }
}
