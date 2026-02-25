using JMChamagas.Application.Abstractions;
using JMChamagas.Domain.Entities;
using JMChamagas.Domain.Enums;
using JMChamagas.Infrastructure.Persistence;
using JMChamagas.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace JMChamagas.Infrastructure.Repositories;

public sealed class ProdutoRepository : IProdutoRepository
{
    private readonly JMChamagasDbContext _db;

    public ProdutoRepository(JMChamagasDbContext db)
    {
        _db = db;
    }

    public async Task<IReadOnlyCollection<Produto>> ListarAsync(CancellationToken cancellationToken = default)
    {
        var rows = await _db.Produtos
            .AsNoTracking()
            .OrderBy(p => p.ProdutoTipo)
            .ThenBy(p => p.ValorUnitario)
            .ToListAsync(cancellationToken);

        return rows
            .Select(MapToDomain)
            .ToList();
    }

    public async Task AdicionarAsync(Produto produto, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(produto);

        if (produto.Id == Guid.Empty)
            produto.Id = Guid.NewGuid();

        var row = new ProdutoRow
        {
            Id = produto.Id,
            ProdutoTipo = (int)produto.ProdutoTipo,
            ValorUnitario = produto.ValorUnitario,
        };

        _db.Produtos.Add(row);
        await _db.SaveChangesAsync(cancellationToken);
    }

    private static Produto MapToDomain(ProdutoRow row)
    {
        var produto = new Produto(row.ValorUnitario, (TipoProduto)row.ProdutoTipo)
        {
            Id = row.Id
        };

        return produto;
    }
}
