using APICatalogo.Controllers;
using APICatalogo.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogoXUnitTests.UnitTests.Produtos;

public class PostProdutosUnitTests : IClassFixture<ProdutosUnitTestsController>
{
    private readonly ProdutosController _controller;

    public PostProdutosUnitTests(ProdutosUnitTestsController controller)
    {
        _controller = new ProdutosController(controller.repository, controller.mapper);
    }

    [Fact]
    public async Task PostProduto_Return_CreatedStatusCode()
    {
        //Arrange
        var produto = new ProdutoDTO()
        {
            Nome = "Produto Teste",
            Descricao = "Descrição do Produto Teste",
            Preco = 100,
            ImagemUrl = "http://teste.net/1.jpg",
            CategoriaId = 1
        };

        //Act
        var data = await _controller.Post(produto);

        //Assert (FluentAssertions)
        var createdResult = data.Result.Should().BeOfType<CreatedAtRouteResult>()
                            .Which.StatusCode.Should().Be(201);
    }

    [Fact]
    public async Task PostProduto_Return_BadRequest()
    {
        //Arrange
        ProdutoDTO produto = null;

        //Act
        var data = await _controller.Post(produto);

        //Assert (FluentAssertions)
        var createdResult = data.Result.Should().BeOfType<BadRequestResult>()
                            .Which.StatusCode.Should().Be(400);
    }
}
