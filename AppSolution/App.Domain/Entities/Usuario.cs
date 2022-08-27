using System;
using System.Collections.Generic;

namespace App.Domain.Entities
{
    public class Usuario //: IdentityUser
    {
        //public Usuario()
        //{
        //    //Endereco = new HashSet<Endereco>();
        //    Pedidos = new HashSet<Pedido>();
        //}



        public Guid Codigo { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public int Sexo { get; set; }

        public virtual ICollection<Endereco> Endereco { get; set; }
        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
