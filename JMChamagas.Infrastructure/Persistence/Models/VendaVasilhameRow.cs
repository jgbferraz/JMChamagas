namespace JMChamagas.Infrastructure.Persistence.Models;

public sealed class VendaVasilhameRow
{
    public Guid Id { get; set; }

    public Guid VendaId { get; set; }
    public VendaRow? Venda { get; set; }

    public int TipoVasilhame { get; set; }
    public decimal ValorUnitario { get; set; }
}
