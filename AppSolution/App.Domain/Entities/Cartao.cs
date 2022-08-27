using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace App.Domain.Entities
{
    public class Cartao
    {
        public int CodigoCartao { get; set; }
        public Guid CodigoUsuario { get; set; }
        public string NumeroCatao { get; set; }
        public string HashCartao { get; set; }
        public DateTime DataVencimentoCartao { get; set; }
        public int SenhaCartao { get; set; }

    }
}
