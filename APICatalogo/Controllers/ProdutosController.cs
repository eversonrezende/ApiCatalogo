using APICatalogo.Models;
using APICatalogo.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IUnitOfWork _uof;
    private readonly ILogger<ProdutosController> _logger;

    public ProdutosController(IUnitOfWork uof, ILogger<ProdutosController> logger)
    {
        _uof = uof;
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Produto>> Get()
    {
        var produtos = _uof.ProdutoRepository.GetAll();

        if (produtos is null)
        {
            _logger.LogWarning("Produtos não encontrados...");
            return NotFound("Produtos não encontrados...");
        }

        return Ok(produtos);
    }

    [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
    public ActionResult<Produto> Get(int id)
    {
        var produto = _uof.ProdutoRepository.Get(p => p.ProdutoId == id);

        if (produto is null)
        {
            _logger.LogWarning($"Produto com id = {id} não encontrado...");
            return NotFound("Produto não encontrado...");
        }

        return Ok(produto);
    }

    [HttpGet("categoria/{id}")]
    public ActionResult<IEnumerable<Produto>> GetProdutosPorCategoria(int id)
    {
        var produtos = _uof.ProdutoRepository.GetProdutoPorCategoria(id);
        if (produtos is null)
        {
            _logger.LogWarning($"Produtos da categoria com id = {id} não encontrados...");
            return NotFound("Produtos não encontrados...");
        }
        return Ok(produtos);
    }

    [HttpPost]
    public ActionResult Post(Produto produto)
    {
        if (produto is null)
        {
            _logger.LogWarning("Produto nulo...");
            return BadRequest();
        }

        var produtoCriado = _uof.ProdutoRepository.Create(produto);
        _uof.Commit();

        return new CreatedAtRouteResult("ObterProduto", new { id = produtoCriado.ProdutoId }, produtoCriado);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Produto produto)
    {
        if (id != produto.ProdutoId)
        {
            _logger.LogWarning($"Produto com id = {id} não encontrado...");
            return BadRequest();
        }

        var produtoAtualizado = _uof.ProdutoRepository.Update(produto);
        _uof.Commit();

        if (produtoAtualizado is not null)
            return Ok(produto);
        else
            return StatusCode(500, $"Falha ao atualizar o produto de id = {id}");
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var produto = _uof.ProdutoRepository.Get(p => p.ProdutoId == id);

        if (produto is null)
        {
            _logger.LogWarning($"Produto com id = {id} não encontrado...");
            return NotFound("Produto não encontrado...");
        }

        var produtoExcluido = _uof.ProdutoRepository.Delete(produto);
        _uof.Commit();

        if (produtoExcluido is not null)
            return Ok($"O produto de id = {id} foi excluído");
        else
            return StatusCode(500, $"Falha ao excluir o produto de id = {id}");
    }
}
