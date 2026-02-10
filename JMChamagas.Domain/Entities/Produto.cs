
using JMChamagas.Domain.Enums;
using JMChamagas.Domain.Interfaces;

namespace JMChamagas.Domain.Entities
{
    public class Produto : JMChamagasBase, IAgregateRoot
    {
        public string Nome { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public decimal PrecoCusto { get; set; }
        public decimal PrecoVenda { get; set; }
       
        public int QuantidadeEstoque { get; set; }
        public int QuantidadeEstoqueMinimo { get; set; }
        public int QuantidadeVasilhame { get; set; }
        public TipoProduto Tipo { get; set; }
        public Vasilhame? Vasilhame { get; set; }


    }

}
