using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace App.Domain.Entities
{
    public class PedidoItem
    {
        //public PedidoItem()
        //{
        //    Produtos = new HashSet<Produto>();
        //}
        public int CodigoPedido { get; set; }
        public int CodigoPedidoItem { get; set; }
        public int CodigoProduto { get; set; }
        public string DescricaoProduto { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get; set; }

        //public virtual IList<Produto> Produtos { get; set; }

    }
}
