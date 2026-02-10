using JMChamagas.Domain.Enums;
using JMChamagas.Domain.Interfaces;

namespace JMChamagas.Domain.Entities
{
    public class Venda : JMChamagasBase, IAgregateRoot
    {
        public Guid FornecedorId { get; set; }
        public Fornecedor? Fornecedor { get; set; }
        public DateTime DataVenda { get; set; }
        public TipoVasilhame TipoVasilhame { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorTotal { get; set; }
    }
}