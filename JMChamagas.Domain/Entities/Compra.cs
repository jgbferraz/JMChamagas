using JMChamagas.Domain.Enums;
using JMChamagas.Domain.Interfaces;

namespace JMChamagas.Domain.Entities
{
    public class Compra : JMChamagasBase, IAgregateRoot
    {
        public Guid FornecedorId { get; set; }
        public Fornecedor? Fornecedor { get; set; }
        public DateTime DataCompra { get; set; }
        public List<Produto>? Produtos { get; set; }
        public List<Vasilhame>? Vasilhames { get; set; }
        public decimal ValorTotal { get; set; }
    }
}