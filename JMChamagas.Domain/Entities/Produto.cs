using JMChamagas.Domain.Enums;
using JMChamagas.Domain.Interfaces;

namespace JMChamagas.Domain.Entities
{
    public class Produto : JMChamagasBase, IAgregateRoot
    {
        public decimal ValorUnitario { get; set; }
        public TipoProduto ProdutoTipo { get; set; }
        public Estoque? Estoque { get; set;} 


    }

}
