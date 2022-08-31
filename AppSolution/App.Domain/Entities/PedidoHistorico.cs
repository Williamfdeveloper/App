using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace App.Domain.Entities
{
    public class PedidoHistorico
    {
        public int CodigoPedidoHistorico { get; set; }
        public int CodigoPedido { get; set; }
        public int IdSituacaoPedido { get; set; }
        public DateTime DataSituacao { get; set; }
        public DateTime DataAtualizacaoInicio { get; set; }
        public DateTime DataAtualizacaoFim { get; set; }

        public virtual Pedido Pedido { get; set; }

    }
}
