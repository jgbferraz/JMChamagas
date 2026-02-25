namespace JMChamagas.Infrastructure.Persistence.Models;

public sealed class VendaProdutoRow
{
    public Guid Id { get; set; }

    public Guid VendaId { get; set; }
    public VendaRow? Venda { get; set; }

    public int ProdutoTipo { get; set; }
    public decimal ValorUnitario { get; set; }
}
