using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entities.Cartao
{
    public class CartaoModel
    {
        public string NumeroCartao { get; set; }
        public string NomeCartao { get; set; }
        public string DataVencimentoCartao { get; set; }
        public string SenhaCartao { get; set; }
    }
}
