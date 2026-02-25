using JMChamagas.Application.Abstractions;
using JMChamagas.Domain.Entities;
using JMChamagas.Domain.Enums;

namespace JMChamagas.Application.Services;

public sealed class ProdutoAppService
{
    private readonly IProdutoRepository _repository;

    public ProdutoAppService(IProdutoRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyCollection<Produto>> ListarAsync(CancellationToken cancellationToken = default) =>
        _repository.ListarAsync(cancellationToken);

    public async Task<Produto> AdicionarAsync(decimal valorUnitario, TipoProduto tipoProduto, CancellationToken cancellationToken = default)
    {
        var produto = new Produto(valorUnitario, tipoProduto);
        if (produto.Id == Guid.Empty)
            produto.Id = Guid.NewGuid();

        await _repository.AdicionarAsync(produto, cancellationToken);
        return produto;
    }
}
