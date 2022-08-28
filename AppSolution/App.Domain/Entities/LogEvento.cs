using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entities
{
    public class LogEvento
    {
        public int id { get; set; }
        public int codigoErro { get; set; }
        public string message { get; set; }
        public string exception { get; set; }
        public DateTime? createdtime { get; set; }

    }
}
