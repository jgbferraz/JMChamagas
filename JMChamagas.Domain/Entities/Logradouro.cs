using JMChamagas.Domain.Enums;
using JMChamagas.Domain.Interfaces;

namespace JMChamagas.Domain.Entities
{
    public class Logradouro : JMChamagasBase, IAgregateRoot
    {
        public string? Rua { get; set; }
        public int Numero { get; set; }
        public string? Complemento { get; set; }
        public int Cep { get; set; }
        public string? Bairro { get; set; }
        public string? Cidade { get; set; }
        public string? Telefone { get; set; }
    }
}