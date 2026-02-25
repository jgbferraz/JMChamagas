using JMChamagas.Domain.Entities;
using JMChamagas.Domain.Enums;

namespace JMChamagas.Web.Services;

public class ProdutoAppService
{
    private readonly List<Produto> _produtos = [];

    public IReadOnlyCollection<Produto> Listar() => _produtos.AsReadOnly();

    public Produto Adicionar(decimal valorUnitario, TipoProduto tipoProduto)
    {
        var produto = new Produto(valorUnitario, tipoProduto);
        _produtos.Add(produto);
        return produto;
    }
}
