using JMChamagas.Application.Abstractions;
using JMChamagas.Domain.Entities;

namespace JMChamagas.Application.Services;

public sealed class VendaAppService
{
    private readonly IVendaRepository _repository;

    public VendaAppService(IVendaRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyCollection<Venda>> ListarAsync(CancellationToken cancellationToken = default) =>
        _repository.ListarAsync(cancellationToken);

    public async Task AdicionarAsync(Venda venda, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(venda);
        if (venda.Id == Guid.Empty)
            venda.Id = Guid.NewGuid();

        await _repository.AdicionarAsync(venda, cancellationToken);
    }
}
