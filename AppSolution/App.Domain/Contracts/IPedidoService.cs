using App.Domain.Entities;
using System.Threading.Tasks;

namespace App.Domain.Contracts
{
    public interface IPedidoService
    {
        Task<bool> CaptarPedido(ref Pedido Pedido);
        Task<Pedido> ConsultarPedido(int CodigoPedido);
    }
}
