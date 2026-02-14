using JMChamagas.Domain.Enums;
using JMChamagas.Domain.Interfaces;

namespace JMChamagas.Domain.Entities
{
    public class Estoque : JMChamagasBase, IAgregateRoot
    {
       
        public int QuantidadeProduto { get; set; }
        public int QuantidadeVasilhame { get; set; }
        public TipoProduto Produto { get; set; }
        public TipoVasilhame Vasilhame { get; set; }

    }
}