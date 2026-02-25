using JMChamagas.Domain.Entities;
using JMChamagas.Domain.Enums;
using JMChamagas.Infrastructure.Persistence;
using JMChamagas.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace JMChamagas.Infrastructure.Database;

public sealed class DatabaseInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public DatabaseInitializer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<JMChamagasDbContext>();

        await db.Database.EnsureCreatedAsync(cancellationToken);

        if (await db.Vendas.AnyAsync(cancellationToken))
            return;

        var venda1 = new Venda(DateTime.UtcNow.AddHours(-3));
        venda1.AdicionarProduto(new Produto(110m, TipoProduto.GasP13));
        venda1.AdicionarVasilhame(new Vasilhame { ValorUnitario = 35m, Tipo = TipoVasilhame.BujaoP13 });

        var venda2 = new Venda(DateTime.UtcNow.AddDays(-1));
        venda2.AdicionarProduto(new Produto(12m, TipoProduto.AguaMineral));

        var venda3 = new Venda(DateTime.UtcNow.AddDays(-2));
        venda3.AdicionarProduto(new Produto(40m, TipoProduto.Carvao));

        var rows = new[] { venda1, venda2, venda3 }
            .Select(v => new VendaRow
            {
                Id = Guid.NewGuid(),
                DataVendaUtc = v.DataVenda.Kind == DateTimeKind.Utc ? v.DataVenda : v.DataVenda.ToUniversalTime(),
                ValorTotal = v.ValorTotal,
                Produtos = v.Produtos.Select(p => new VendaProdutoRow
                {
                    Id = Guid.NewGuid(),
                    ProdutoTipo = (int)p.ProdutoTipo,
                    ValorUnitario = p.ValorUnitario,
                }).ToList(),
                Vasilhames = v.Vasilhames.Select(x => new VendaVasilhameRow
                {
                    Id = Guid.NewGuid(),
                    TipoVasilhame = (int)x.Tipo,
                    ValorUnitario = x.ValorUnitario,
                }).ToList(),
            })
            .ToList();

        db.Vendas.AddRange(rows);
        await db.SaveChangesAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
