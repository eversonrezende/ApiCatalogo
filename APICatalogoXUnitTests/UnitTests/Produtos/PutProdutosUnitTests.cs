using APICatalogo.Controllers;
using APICatalogo.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogoXUnitTests.UnitTests.Produtos;

public class PutProdutosUnitTests : IClassFixture<ProdutosUnitTestsController>
{
    private readonly ProdutosController _controller;

    public PutProdutosUnitTests(ProdutosUnitTestsController controller)
    {
        _controller = new ProdutosController(controller.repository, controller.mapper);
    }

    [Fact]
    public async Task PutProduto_Return_OkResult()
    {
        //Arrange
        var prodId = 1;

        var produto = new ProdutoDTO()
        {
            ProdutoId = 1,
            Nome = "Produto Teste",
            Descricao = "Descrição do Produto Teste",
            Preco = 100,
            ImagemUrl = "http://teste.net/1.jpg",
            CategoriaId = 1
        };

        //Act
        var data = await _controller.Put(prodId, produto);

        //Assert (FluentAssertions)
        data.Should().NotBeNull();
        data.Result.Should().BeOfType<OkObjectResult>()
            .Which.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task PutProduto_Return_BadRequest()
    {
        //Arrange
        var prodId = 1;

        var produto = new ProdutoDTO()
        {
            ProdutoId = 2,
            Nome = "Produto Teste",
            Descricao = "Descrição do Produto Teste",
            Preco = 100,
            ImagemUrl = "http://teste.net/1.jpg",
            CategoriaId = 1
        };

        //Act
        var data = await _controller.Put(prodId, produto);

        //Assert (FluentAssertions)
        data.Result.Should().BeOfType<BadRequestResult>()
            .Which.StatusCode.Should().Be(400);
    }
}
