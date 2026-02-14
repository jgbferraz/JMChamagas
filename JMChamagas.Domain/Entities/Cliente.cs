using JMChamagas.Domain.Enums;
using JMChamagas.Domain.Interfaces;

namespace JMChamagas.Domain.Entities
{

    public class Cliente : JMChamagasBase, IAgregateRoot
    {

        public string? Nome { get; set; }
        public Logradouro? Logradouro { get; set; }
        public TipoCliente Tipo { get; set; }
    }
}