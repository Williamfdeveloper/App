using App.Domain.Entities;
using System;

namespace App.Domain.Entities.Register
{
    public class Register
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public int Sexo { get; set; }
        //public string Rua { get; set; }
        //public string Numero { get; set; }
        //public string Bairro { get; set; }
        //public string Cidade { get; set; }
        //public string Estado { get; set; }
        //public string CEP { get; set; }
        public virtual Endereco Endereco { get; set; }
    }
}



