using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace App.Domain.Entities
{
    public class Usuario : IdentityUser
    {

        public string Nome { get; set; }
        
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public int Sexo { get; set; }

        public virtual IList<Endereco> Enderecos { get; set; }
        public virtual IList<DadosCartao> Cartoes { get; set; }
        public virtual IList<Pedido> Pedidos { get; set; }
    }
}
