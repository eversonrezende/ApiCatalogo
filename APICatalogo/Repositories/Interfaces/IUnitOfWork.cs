﻿namespace APICatalogo.Repositories.Interfaces;

public interface IUnitOfWork
{
    IProdutosRepository ProdutoRepository { get; }
    ICategoriaRepository CategoriaRepository { get; }
    void Commit();
}
