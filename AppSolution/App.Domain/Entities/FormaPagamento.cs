using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace App.Domain.Entities
{
    public class FormaPagamento
    {
        public int CodigoFormaPagamento { get; set; }
        public string DescricaoFormaPagamento { get; set; }
        public int QuantidadeParcelas { get; set; }
        public decimal ValorDesconto { get; set; }
        public int TipoDesconto { get; set; }
        public int IdTipoFormaPagamento{ get; set; }
        public bool Ativa { get; set; }


        public virtual IList<Pedido> Pedidos { get; set; }
    }
}
