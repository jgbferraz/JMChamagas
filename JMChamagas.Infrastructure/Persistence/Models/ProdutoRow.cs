namespace JMChamagas.Infrastructure.Persistence.Models;

public sealed class ProdutoRow
{
    public Guid Id { get; set; }
    public decimal ValorUnitario { get; set; }
    public int ProdutoTipo { get; set; }
}
