using CasaDoCodigo.Models;

namespace CasaDoCodigo.Repositories
{
    public interface IPedidoRepository
    {
        Pedido GetPedido();
        Pedido GetPedido(int p);
        void AddItem(string codigo);
    }
}