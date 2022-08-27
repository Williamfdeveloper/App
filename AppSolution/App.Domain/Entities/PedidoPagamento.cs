using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace App.Domain.Entities
{
    public class PedidoPagamento
    {
        public int CodigoPedidoPagamento { get; set; }
        public int CodigoPedido { get; set; }
        public int IdSituacaoPagamento { get; set; }
        public DateTime DataPagamento { get; set; }
        public DateTime DataAprovado { get; set; }
        public DateTime DataAtualizacao { get; set; }

    }
}
