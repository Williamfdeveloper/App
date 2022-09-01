using App.Domain.Entities;
using App.Domain.Entities.Cartao;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Contracts
{
    public interface ISchedulerService
    {
        Task<bool> ProcessarPedidoFila(CartaoModel cartao, ref Pedido pedido);
    }
}
