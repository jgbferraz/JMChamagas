namespace JMChamagas.Infrastructure.Persistence.Models;

public sealed class VendaRow
{
    public Guid Id { get; set; }
    public DateTime DataVendaUtc { get; set; }
    public decimal ValorTotal { get; set; }

    public List<VendaProdutoRow> Produtos { get; set; } = [];
    public List<VendaVasilhameRow> Vasilhames { get; set; } = [];
}
