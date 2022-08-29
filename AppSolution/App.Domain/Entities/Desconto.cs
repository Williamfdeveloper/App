using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entities
{
    public partial class Desconto
    {
        public int descontoid { get; set; }
        public string codigodesconto { get; set; }
        public int tipodesconto { get; set; }
        public float valor { get; set; }
        public int quantidadeporusuario { get; set; }
        public int quantidadedisponivel { get; set; }
        public DateTime validade { get; set; }
        public string regra { get; set; }
        public bool ativo { get; set; }
        //public bool cupomunitrario { get; set; }
    }
}
