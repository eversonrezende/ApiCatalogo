using APICatalogo.Controllers;
using APICatalogo.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogoXUnitTests.UnitTests.Produtos;

public class GetProdutoUnitTests : IClassFixture<ProdutosUnitTestsController>
{
    private readonly ProdutosController _controller;

    public GetProdutoUnitTests(ProdutosUnitTestsController controller)
    {
        _controller = new ProdutosController(controller.repository, controller.mapper);
    }

    [Fact]
    public async Task GetProdutosById_OKResult()
    {
        //Arrange
        var prodId = 1;

        //Act
        var data = await _controller.Get(prodId);

        //Assert (xUnit)
        //var okResult = Assert.IsType<OkObjectResult>(data.Result);
        //Assert.Equal(200, okResult.StatusCode);

        //Assert (FluentAssertions)
        data.Result.Should().BeOfType<OkObjectResult>()
                            .Which.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task GetProdutosById_Return_NotFound()
    {
        //Arrange
        var prodId = 999;

        //Act
        var data = await _controller.Get(prodId);

        //Assert (FluentAssertions)
        data.Result.Should().BeOfType<NotFoundObjectResult>()
                            .Which.StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task GetProdutosById_Return_BadRequest()
    {
        //Arrange
        var prodId = -1;

        //Act
        var data = await _controller.Get(prodId);

        //Assert (FluentAssertions)
        data.Result.Should().BeOfType<BadRequestObjectResult>()
                            .Which.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task GetProdutos_Return_ListOfProdutosDTO()
    {
        //Act
        var data = await _controller.Get();

        //Assert (FluentAssertions)
        data.Result.Should().BeOfType<OkObjectResult>()
                            .Which.Value.Should().BeAssignableTo<IEnumerable<ProdutoDTO>>()
                            .And.NotBeNull();
    }

}
