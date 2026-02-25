using JMChamagas.Domain.Entities;

namespace JMChamagas.Application.Abstractions;

public interface IProdutoRepository
{
    Task<IReadOnlyCollection<Produto>> ListarAsync(CancellationToken cancellationToken = default);
    Task AdicionarAsync(Produto produto, CancellationToken cancellationToken = default);
}
