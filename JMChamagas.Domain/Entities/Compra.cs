using JMChamagas.Domain.Enums;
using JMChamagas.Domain.Interfaces;

namespace JMChamagas.Domain.Entities
{
    public class Compra : JMChamagasBase, IAgregateRoot
    {
        public Compra()
        {
        }

        public Compra(Guid fornecedorId, DateTime dataCompra)
        {
            DefinirFornecedor(fornecedorId);
            DefinirDataCompra(dataCompra);
        }

        public Guid FornecedorId { get; set; }
        public Fornecedor? Fornecedor { get; set; }
        public DateTime DataCompra { get; private set; }
        public List<Produto> Produtos { get; private set; } = [];
        public List<Vasilhame> Vasilhames { get; private set; } = [];
        public decimal ValorTotal { get; private set; }

        public void DefinirFornecedor(Guid fornecedorId)
        {
            if (fornecedorId == Guid.Empty)
            {
                throw new ArgumentException("Fornecedor inválido.", nameof(fornecedorId));
            }

            FornecedorId = fornecedorId;
        }

        public void DefinirDataCompra(DateTime dataCompra)
        {
            if (dataCompra > DateTime.UtcNow)
            {
                throw new ArgumentOutOfRangeException(nameof(dataCompra), "A data da compra não pode estar no futuro.");
            }

            DataCompra = dataCompra;
        }

        public void AdicionarProduto(Produto produto)
        {
            ArgumentNullException.ThrowIfNull(produto);
            Produtos.Add(produto);
            RecalcularValorTotal();
        }

        public void AdicionarVasilhame(Vasilhame vasilhame)
        {
            ArgumentNullException.ThrowIfNull(vasilhame);
            Vasilhames.Add(vasilhame);
            RecalcularValorTotal();
        }

        public void RemoverProduto(Produto produto)
        {
            ArgumentNullException.ThrowIfNull(produto);

            if (Produtos.Remove(produto))
            {
                RecalcularValorTotal();
            }
        }

        public void RemoverVasilhame(Vasilhame vasilhame)
        {
            ArgumentNullException.ThrowIfNull(vasilhame);

            if (Vasilhames.Remove(vasilhame))
            {
                RecalcularValorTotal();
            }
        }

        private void RecalcularValorTotal()
        {
            ValorTotal = Produtos.Sum(produto => produto.ValorUnitario) + Vasilhames.Sum(vasilhame => vasilhame.ValorUnitario);
        }
    }
}