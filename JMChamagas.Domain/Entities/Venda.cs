using JMChamagas.Domain.Interfaces;

namespace JMChamagas.Domain.Entities
{
    public class Venda : JMChamagasBase, IAgregateRoot
    {
        public Venda()
        {
        }

        public Venda(DateTime dataVenda)
        {
            DefinirDataVenda(dataVenda);
        }

        public DateTime DataVenda { get; private set; }
        public List<Produto> Produtos { get; private set; } = [];
        public List<Vasilhame> Vasilhames { get; private set; } = [];
        public decimal ValorTotal { get; private set; }

        public void DefinirDataVenda(DateTime dataVenda)
        {
            if (dataVenda > DateTime.UtcNow)
            {
                throw new ArgumentOutOfRangeException(nameof(dataVenda), "A data da venda não pode estar no futuro.");
            }

            DataVenda = dataVenda;
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