using App.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Contracts.Repository
{
    public interface IPedidoRepository
    {
        bool Salvar(ref Pedido Pedido);
        bool Atualizar(ref Pedido Pedido);
        Pedido Buscar(int CodigoPedido);
        IList<Pedido> BuscarLista();
        IList<Pedido> BuscarListaPorUsuario(string UsuarioID);
    }
}
