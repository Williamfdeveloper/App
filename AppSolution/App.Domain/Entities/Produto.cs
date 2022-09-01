using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace App.Domain.Entities
{
    public class Produto
    {
        public int CodigoProduto { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public string LinkDownload { get; set; }

        public virtual ICollection<PedidoItem> ItensPedido { get; set; }

    }
}
