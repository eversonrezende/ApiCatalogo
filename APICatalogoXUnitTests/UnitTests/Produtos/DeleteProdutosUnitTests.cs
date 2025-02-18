using APICatalogo.Controllers;
using APICatalogo.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogoXUnitTests.UnitTests.Produtos;

public class DeleteProdutosUnitTests : IClassFixture<ProdutosUnitTestsController>
{
    private readonly ProdutosController _controller;

    public DeleteProdutosUnitTests(ProdutosUnitTestsController controller)
    {
        _controller = new ProdutosController(controller.repository, controller.mapper);
    }

    [Fact]
    public async Task DeleteProduto_Return_OkResult()
    {
        //Arrange
        var prodId = 12;

        //Act
        var data = await _controller.Delete(prodId) as ActionResult<ProdutoDTO>;

        //Assert (FluentAssertions)
        data.Result.Should().BeOfType<OkObjectResult>()
                            .Which.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task DeleteProduto_Return_NotFound()
    {
        //Arrange
        var prodId = 999;

        //Act
        var data = await _controller.Delete(prodId) as ActionResult<ProdutoDTO>;

        //Assert (FluentAssertions)
        data.Result.Should().BeOfType<NotFoundObjectResult>()
                            .Which.StatusCode.Should().Be(404);
    }
}
