using APICatalogo.Context;
using APICatalogo.DTOs.Mappings;
using APICatalogo.Repositories;
using APICatalogo.Repositories.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace APICatalogoXUnitTests.UnitTests;

public class ProdutosUnitTestsController
{
    public IUnitOfWork repository;
    public IMapper mapper;
    public static DbContextOptions<AppDbContext> dbContextOptions { get; }

    public static string connectionString =
        "Server=localhost;DataBase=CatalogoDB;Uid=root;Pwd=root";

    static ProdutosUnitTestsController()
    {
        dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            .Options;
    }

    public ProdutosUnitTestsController()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new DTOMappingProfile());
        });

        mapper = config.CreateMapper();

        var context = new AppDbContext(dbContextOptions);
        repository = new UnitOfWork(context);
    }


}
