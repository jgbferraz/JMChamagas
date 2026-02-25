using JMChamagas.Domain.Enums;
using JMChamagas.Domain.Interfaces;

namespace JMChamagas.Domain.Entities
{
    public class Produto : JMChamagasBase, IAgregateRoot
    {
        public Produto()
        {
        }

        public Produto(decimal valorUnitario, TipoProduto produtoTipo)
        {
            AtualizarValorUnitario(valorUnitario);
            DefinirTipo(produtoTipo);
        }

        public decimal ValorUnitario { get; private set; }
        public TipoProduto ProdutoTipo { get; private set; }
        public Estoque? Estoque { get; private set; }

        public void AtualizarValorUnitario(decimal valorUnitario)
        {
            if (valorUnitario <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(valorUnitario), "O valor unitário deve ser maior que zero.");
            }

            ValorUnitario = valorUnitario;
        }

        public void DefinirTipo(TipoProduto produtoTipo)
        {
            if (!Enum.IsDefined(produtoTipo))
            {
                throw new ArgumentException("Tipo de produto inválido.", nameof(produtoTipo));
            }

            ProdutoTipo = produtoTipo;
        }

        public void VincularEstoque(Estoque estoque)
        {
            ArgumentNullException.ThrowIfNull(estoque);
            Estoque = estoque;
        }

        public void RemoverEstoque()
        {
            Estoque = null;
        }

    }

}
