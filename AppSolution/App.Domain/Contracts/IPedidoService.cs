using App.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace App.Domain.Contracts
{
    public interface IPedidoService
    {
        bool AdicionarItemCarrinho(int produtoID, int quantidade, Usuario usuario, ref Pedido pedido);
    }
}
