using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace App.Domain.Entities
{
    public class CustomException : Exception
    {
        public HttpStatusCode httpStatusCode { get; set; }
        public string mensagemErro { get; set; }
        public string ActionReturn { get; set; }
    }
}
