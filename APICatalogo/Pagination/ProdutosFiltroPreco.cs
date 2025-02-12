namespace APICatalogo.Pagination;

public class ProdutosFiltroPreco : QueryStringParameter
{
    public decimal? Preco { get; set; }
    public string? PrecoCriterio { get; set; }
}
