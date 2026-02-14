using JMChamagas.Domain.Enums;
using JMChamagas.Domain.Interfaces;

namespace JMChamagas.Domain.Entities
{
    public class Fornecedor : JMChamagasBase, IAgregateRoot
    {
        public string Nome { get; set; } = string.Empty;
        public string CNPJ { get; set; } = string.Empty;
        public Logradouro? Logradouro { get; set; }
        public Produto? Produto { get; set; }
        public Vasilhame? Vasilhame { get; set; }
    }
}
