using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain
{
    public class Constant
    {
        public enum FormaPagamento
        {
            Boleto = 1,
            Credito = 2,
            Debito = 3
        }

        public enum SituacaoPedido
        {
            EmCaptacao = 1,
            Finalizado = 2,
            Cancelado = 3,
            Entregue = 4
        }

        public enum SituacaoPedidoPagamento
        {
            Iniciada = 1,
            EmAprovacao = 2,
            Aprovado = 3,
            Cancelado = 4
        }

        public enum Sexo
        {
            Masculino = 1,
            Feminino = 2,
            Outros = 3
        }
    }
}
