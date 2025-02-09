using APICatalogo.DTOs;
using APICatalogo.DTOs.Produtos;
using APICatalogo.Models;
using APICatalogo.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IUnitOfWork _uof;
    private readonly ILogger<ProdutosController> _logger;
    private readonly IMapper _mapper;

    public ProdutosController(IUnitOfWork uof, ILogger<ProdutosController> logger, IMapper mapper)
    {
        _uof = uof;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ProdutoDTO>> Get()
    {
        var produtos = _uof.ProdutoRepository.GetAll();

        if (produtos is null)
        {
            _logger.LogWarning("Produtos não encontrados...");
            return NotFound("Produtos não encontrados...");
        }

        var produtosDTO = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

        return Ok(produtosDTO);
    }

    [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
    public ActionResult<ProdutoDTO> Get(int id)
    {
        var produto = _uof.ProdutoRepository.Get(p => p.ProdutoId == id);

        if (produto is null)
        {
            _logger.LogWarning($"Produto com id = {id} não encontrado...");
            return NotFound("Produto não encontrado...");
        }

        var produtoDTO = _mapper.Map<ProdutoDTO>(produto);

        return Ok(produtoDTO);
    }

    [HttpGet("categoria/{id}")]
    public ActionResult<IEnumerable<ProdutoDTO>> GetProdutosPorCategoria(int id)
    {
        var produtos = _uof.ProdutoRepository.GetProdutoPorCategoria(id);

        if (produtos is null)
        {
            _logger.LogWarning($"Produtos da categoria com id = {id} não encontrados...");
            return NotFound("Produtos não encontrados...");
        }

        var produtosDTO = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

        return Ok(produtosDTO);
    }

    [HttpPost]
    public ActionResult<ProdutoDTO> Post(ProdutoDTO produtoDto)
    {
        if (produtoDto is null)
        {
            _logger.LogWarning("Produto nulo...");
            return BadRequest();
        }

        var produto = _mapper.Map<Produto>(produtoDto);

        var produtoCriado = _uof.ProdutoRepository.Create(produto);
        _uof.Commit();

        var novoProdutoDTO = _mapper.Map<ProdutoDTO>(produtoCriado);

        return new CreatedAtRouteResult("ObterProduto", new { id = novoProdutoDTO.ProdutoId }, novoProdutoDTO);
    }

    [HttpPatch("{id}/UpdateParcial")]
    public ActionResult<ProdutoDTOUpdateResponse> Patch(int id,
        JsonPatchDocument<ProdutoDTOUpdateRequest> patchProdutoDTO)
    {
        if (patchProdutoDTO is null || id <= 0)
            return BadRequest();

        var produto = _uof.ProdutoRepository.Get(p => p.ProdutoId == id);

        if (produto is null)
            return NotFound();

        var produtoUpdateRequest = _mapper.Map<ProdutoDTOUpdateRequest>(produto);

        patchProdutoDTO.ApplyTo(produtoUpdateRequest, ModelState);

        if (!ModelState.IsValid || TryValidateModel(produtoUpdateRequest))
            return BadRequest(ModelState);

        _mapper.Map(produtoUpdateRequest, produto);

        _uof.ProdutoRepository.Update(produto);
        _uof.Commit();

        return Ok(_mapper.Map<ProdutoDTOUpdateResponse>(produto));
    }


    [HttpPut("{id:int}")]
    public ActionResult<ProdutoDTO> Put(int id, ProdutoDTO produto)
    {
        if (id != produto.ProdutoId)
        {
            _logger.LogWarning($"Produto com id = {id} não encontrado...");
            return BadRequest();
        }

        var produtoExistente = _mapper.Map<Produto>(produto);

        var produtoAtualizado = _uof.ProdutoRepository.Update(produtoExistente);
        _uof.Commit();

        if (produtoAtualizado is not null)
        {
            var produtoDTO = _mapper.Map<ProdutoDTO>(produtoAtualizado);

            return Ok(produtoDTO);
        }
        else
            return StatusCode(500, $"Falha ao atualizar o produto de id = {id}");
    }

    [HttpDelete("{id:int}")]
    public ActionResult<ProdutoDTO> Delete(int id)
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
        {
            var produtoDTO = _mapper.Map<ProdutoDTO>(produtoExcluido);
            return Ok(produtoDTO);
        }
        else
            return StatusCode(500, $"Falha ao excluir o produto de id = {id}");
    }
}
