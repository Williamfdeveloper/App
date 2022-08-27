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
        public int IdTipoFormaPagamento{ get; set; }

    }
}
