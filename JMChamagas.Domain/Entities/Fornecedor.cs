using JMChamagas.Domain.Enums;
using JMChamagas.Domain.Interfaces;

namespace JMChamagas.Domain.Entities
{
    public class Fornecedor : JMChamagasBase, IAgregateRoot
    {
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public TipoCliente TipoCliente { get; set; }
    }
}
