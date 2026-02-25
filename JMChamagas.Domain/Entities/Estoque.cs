using JMChamagas.Domain.Enums;
using JMChamagas.Domain.Interfaces;

namespace JMChamagas.Domain.Entities
{
    public class Estoque : JMChamagasBase, IAgregateRoot
    {
        public Estoque()
        {
        }

        public Estoque(TipoProduto produto, TipoVasilhame vasilhame, int quantidadeProduto, int quantidadeVasilhame)
        {
            DefinirProduto(produto);
            DefinirVasilhame(vasilhame);
            DefinirQuantidadeProduto(quantidadeProduto);
            DefinirQuantidadeVasilhame(quantidadeVasilhame);
        }

        public int QuantidadeProduto { get; private set; }
        public int QuantidadeVasilhame { get; private set; }
        public TipoProduto Produto { get; private set; }
        public TipoVasilhame Vasilhame { get; private set; }

        public void DefinirQuantidadeProduto(int quantidadeProduto)
        {
            if (quantidadeProduto < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(quantidadeProduto), "A quantidade de produto não pode ser negativa.");
            }

            QuantidadeProduto = quantidadeProduto;
        }

        public void DefinirQuantidadeVasilhame(int quantidadeVasilhame)
        {
            if (quantidadeVasilhame < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(quantidadeVasilhame), "A quantidade de vasilhame não pode ser negativa.");
            }

            QuantidadeVasilhame = quantidadeVasilhame;
        }

        public void DefinirProduto(TipoProduto produto)
        {
            if (!Enum.IsDefined(produto))
            {
                throw new ArgumentException("Tipo de produto inválido.", nameof(produto));
            }

            Produto = produto;
        }

        public void DefinirVasilhame(TipoVasilhame vasilhame)
        {
            if (!Enum.IsDefined(vasilhame))
            {
                throw new ArgumentException("Tipo de vasilhame inválido.", nameof(vasilhame));
            }

            Vasilhame = vasilhame;
        }

        public void AdicionarProduto(int quantidade)
        {
            if (quantidade <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(quantidade), "A quantidade para entrada deve ser maior que zero.");
            }

            QuantidadeProduto += quantidade;
        }

        public void RemoverProduto(int quantidade)
        {
            if (quantidade <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(quantidade), "A quantidade para saída deve ser maior que zero.");
            }

            if (quantidade > QuantidadeProduto)
            {
                throw new InvalidOperationException("Estoque de produto insuficiente para saída.");
            }

            QuantidadeProduto -= quantidade;
        }

        public void AdicionarVasilhame(int quantidade)
        {
            if (quantidade <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(quantidade), "A quantidade para entrada deve ser maior que zero.");
            }

            QuantidadeVasilhame += quantidade;
        }

        public void RemoverVasilhame(int quantidade)
        {
            if (quantidade <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(quantidade), "A quantidade para saída deve ser maior que zero.");
            }

            if (quantidade > QuantidadeVasilhame)
            {
                throw new InvalidOperationException("Estoque de vasilhame insuficiente para saída.");
            }

            QuantidadeVasilhame -= quantidade;
        }

    }
}