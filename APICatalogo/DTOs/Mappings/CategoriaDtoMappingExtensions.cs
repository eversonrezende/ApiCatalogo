﻿using APICatalogo.Models;

namespace APICatalogo.DTOs.Mappings;

public static class CategoriaDtoMappingExtensions
{
    public static CategoriaDTO? ToCategoriaDto(this Categoria categoria)
    {
        if (categoria is null)
            return null;

        return new CategoriaDTO
        {
            CategoriaId = categoria.CategoriaId,
            Nome = categoria.Nome,
            ImagemUrl = categoria.ImagemUrl
        };
    }

    public static Categoria? ToCategoria(this CategoriaDTO categoriaDto)
    {
        if (categoriaDto is null)
            return null;

        return new Categoria
        {
            CategoriaId = categoriaDto.CategoriaId,
            Nome = categoriaDto.Nome,
            ImagemUrl = categoriaDto.ImagemUrl
        };
    }

    public static IEnumerable<CategoriaDTO> ToCategoriaDTOList(this IEnumerable<Categoria> categorias)
    {
        if (categorias is null || !categorias.Any())
            return new List<CategoriaDTO>();

        List<CategoriaDTO> categoriasDto = new();

        foreach (var categoria in categorias)
        {
            categoriasDto.Add(categoria.ToCategoriaDto()!);
        }
        return categoriasDto;
    }
}
