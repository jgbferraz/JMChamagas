using JMChamagas.Domain.Entities;

namespace JMChamagas.Application.Abstractions;

public interface IVendaRepository
{
    Task<IReadOnlyCollection<Venda>> ListarAsync(CancellationToken cancellationToken = default);
    Task AdicionarAsync(Venda venda, CancellationToken cancellationToken = default);
}
