using JMChamagas.Domain.Enums;
using JMChamagas.Domain.Interfaces;

namespace JMChamagas.Domain.Entities
{
    public class Vasilhame : JMChamagasBase, IAgregateRoot
    {
        public decimal ValorUnitario { get; set; }
        public TipoVasilhame Tipo { get; set; }
        public Estoque? Estoque { get; set; }
    }
}