
using System;
using System.Collections.Generic;

namespace App.Domain.Entities
{
    public class Pedido
    {
        //public Pedido()
        //{
        //    PedidoItem = new HashSet<PedidoItem>();
        //    PedidoHistorico = new HashSet<PedidoHistorico>();
        //}
        public int CodigoPedido { get; set; }
        public string Id { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorTotalComDesconto { get; set; }
        public int QuatidadeItensVenda { get; set; }
        public DateTime DataPedido { get; set; }
        public DateTime DataAprovacaoPedido { get; set; }
        public int SituacaoPedido { get; set; }

        //public virtual IdentityUser usuario { get; set; }
        public virtual ICollection<PedidoItem> PedidoItem { get; set; }
        public virtual ICollection<PedidoHistorico> PedidoHistorico { get; set; }
        public virtual PedidoPagamento PedidoPagamento { get; set; }

    }
}
