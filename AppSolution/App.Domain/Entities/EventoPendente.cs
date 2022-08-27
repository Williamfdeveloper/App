using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entities
{
    public class EventoPendente
    {
        public Guid GuidEvento { get; set; }
        public DateTime DataEvento { get; set; }
        public string DescricaoEvento { get; set; }
        public int IdFilaEvento { get; set; }
        public DateTime DataProcessamentoEvento { get; set; }


    }
}
