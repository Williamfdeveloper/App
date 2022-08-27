using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entities
{
    public class CustomException : Exception
    {
        public string mensagemErro { get; set; }
        public string ActionReturn { get; set; }
    }
}
