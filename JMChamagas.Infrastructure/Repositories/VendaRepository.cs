using JMChamagas.Application.Abstractions;
using JMChamagas.Domain.Entities;
using JMChamagas.Domain.Enums;
using JMChamagas.Infrastructure.Persistence;
using JMChamagas.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace JMChamagas.Infrastructure.Repositories;

public sealed class VendaRepository : IVendaRepository
{
    private readonly JMChamagasDbContext _db;

    public VendaRepository(JMChamagasDbContext db)
    {
        _db = db;
    }

    public async Task<IReadOnlyCollection<Venda>> ListarAsync(CancellationToken cancellationToken = default)
    {
        var rows = await _db.Vendas
            .AsNoTracking()
            .Include(v => v.Produtos)
            .Include(v => v.Vasilhames)
            .OrderByDescending(v => v.DataVendaUtc)
            .ToListAsync(cancellationToken);

        return rows.Select(MapToDomain).ToList();
    }

    public async Task AdicionarAsync(Venda venda, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(venda);
        if (venda.Id == Guid.Empty)
            venda.Id = Guid.NewGuid();

        var row = new VendaRow
        {
            Id = venda.Id,
            DataVendaUtc = venda.DataVenda.Kind == DateTimeKind.Utc ? venda.DataVenda : venda.DataVenda.ToUniversalTime(),
            ValorTotal = venda.ValorTotal,
            Produtos = venda.Produtos.Select(p => new VendaProdutoRow
            {
                Id = p.Id == Guid.Empty ? Guid.NewGuid() : p.Id,
                ProdutoTipo = (int)p.ProdutoTipo,
                ValorUnitario = p.ValorUnitario,
            }).ToList(),
            Vasilhames = venda.Vasilhames.Select(v => new VendaVasilhameRow
            {
                Id = v.Id == Guid.Empty ? Guid.NewGuid() : v.Id,
                TipoVasilhame = (int)v.Tipo,
                ValorUnitario = v.ValorUnitario,
            }).ToList(),
        };

        _db.Vendas.Add(row);
        await _db.SaveChangesAsync(cancellationToken);
    }

    private static Venda MapToDomain(VendaRow row)
    {
        var venda = new Venda(row.DataVendaUtc)
        {
            Id = row.Id
        };

        foreach (var produtoRow in row.Produtos.OrderBy(p => p.Id))
        {
            var produto = new Produto(produtoRow.ValorUnitario, (TipoProduto)produtoRow.ProdutoTipo)
            {
                Id = produtoRow.Id
            };
            venda.AdicionarProduto(produto);
        }

        foreach (var vasilhameRow in row.Vasilhames.OrderBy(v => v.Id))
        {
            var vasilhame = new Vasilhame
            {
                Id = vasilhameRow.Id,
                Tipo = (TipoVasilhame)vasilhameRow.TipoVasilhame,
                ValorUnitario = vasilhameRow.ValorUnitario,
            };

            venda.AdicionarVasilhame(vasilhame);
        }

        return venda;
    }
}
