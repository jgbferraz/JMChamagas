using JMChamagas.Domain.Enums;
using JMChamagas.Domain.Interfaces;

namespace JMChamagas.Domain.Entities
{
   
public class Cliente : JMChamagasBase, IAgregateRoot
{
  
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string Endereco { get; set; }
    public TipoCliente Tipo { get; set; } // Pessoa Física/Jurídica
}
}