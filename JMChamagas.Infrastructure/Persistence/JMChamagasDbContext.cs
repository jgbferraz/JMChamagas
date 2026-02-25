using JMChamagas.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace JMChamagas.Infrastructure.Persistence;

public sealed class JMChamagasDbContext : DbContext
{
    public JMChamagasDbContext(DbContextOptions<JMChamagasDbContext> options) : base(options)
    {
    }

    public DbSet<ProdutoRow> Produtos => Set<ProdutoRow>();
    public DbSet<VendaRow> Vendas => Set<VendaRow>();
    public DbSet<VendaProdutoRow> VendaProdutos => Set<VendaProdutoRow>();
    public DbSet<VendaVasilhameRow> VendaVasilhames => Set<VendaVasilhameRow>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProdutoRow>(b =>
        {
            b.ToTable("Produtos");
            b.HasKey(x => x.Id);
            b.Property(x => x.ValorUnitario).HasColumnType("decimal(18,2)");
            b.Property(x => x.ProdutoTipo).IsRequired();
        });

        modelBuilder.Entity<VendaRow>(b =>
        {
            b.ToTable("Vendas");
            b.HasKey(x => x.Id);
            b.Property(x => x.DataVendaUtc).IsRequired();
            b.Property(x => x.ValorTotal).HasColumnType("decimal(18,2)");

            b.HasMany(x => x.Produtos)
                .WithOne(x => x.Venda)
                .HasForeignKey(x => x.VendaId)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasMany(x => x.Vasilhames)
                .WithOne(x => x.Venda)
                .HasForeignKey(x => x.VendaId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<VendaProdutoRow>(b =>
        {
            b.ToTable("VendaProdutos");
            b.HasKey(x => x.Id);
            b.Property(x => x.ValorUnitario).HasColumnType("decimal(18,2)");
            b.Property(x => x.ProdutoTipo).IsRequired();
        });

        modelBuilder.Entity<VendaVasilhameRow>(b =>
        {
            b.ToTable("VendaVasilhames");
            b.HasKey(x => x.Id);
            b.Property(x => x.ValorUnitario).HasColumnType("decimal(18,2)");
            b.Property(x => x.TipoVasilhame).IsRequired();
        });
    }
}
