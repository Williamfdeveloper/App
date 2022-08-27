using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entities
{
    public class EmailSistema
    {
        public Guid ID { get; set; }
        public string PrimaryDomain { get; set; }
        public int PrimaryPort { get; set; }
        public string UsernameEmail { get; set; }
        public string UsernamePassword { get; set; }
        public string ServerNameDisplay { get; set; }
        public int TipodEmail { get; set; }
        //public string FromEmail { get; set; }
        //public string ToEmail { get; set; }
        //public string CcEmail { get; set; }
        //public string EmailCopiaOculta { get; set; }
    }
}
