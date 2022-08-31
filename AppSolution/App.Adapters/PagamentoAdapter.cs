using App.Domain.Contracts.Adapter;
using App.Domain.Entities.Cartao;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Adapters
{
    public class PagamentoAdapter : IPagamentoAdapter
    {
        public int EfetuarPagamento(CartaoModel cartao, decimal valor)
        {
            var randomSituacao = new Random();

            return randomSituacao.Next(2,4);
        }
    }
}
