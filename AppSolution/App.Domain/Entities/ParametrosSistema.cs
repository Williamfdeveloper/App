using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace App.Domain.Entities
{
    public partial class ParametrosSistema
    {
        [Key]
        public Guid parametrosid { get; set; }
        public string nome { get; set; }
        public string valor { get; set; }
        public string descricao { get; set; }
        public int tipocampo { get; set; }
    }
}
