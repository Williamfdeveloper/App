using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain
{
    public class EnumTipo
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
            EmAprovacao = 2,
            Finalizado = 3,
            Cancelado = 4,
            Entregue = 5
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

        public enum TipoEmailSistema
        {
            Sistema = 1
        }

        public enum TipoLayout
        {
            TelaCheia = 1
        }
        public enum TipoDesconto
        {
            Porcentagem = 1,
            Valor = 2
        }

        public enum Queue
        {
            Pedido = 1
        }
    }
}
