using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace App.Domain.Entities
{
    public class DadosCartao
    {
        public int CodigoDadosCartao { get; set; }
        public string CodigoUsuario { get; set; }
        public string NumeroCartao { get; set; }
        public string NomeCartao { get; set; }
        public string HashCartao { get; set; }
        public string DataVencimentoCartao { get; set; }

    }
}
