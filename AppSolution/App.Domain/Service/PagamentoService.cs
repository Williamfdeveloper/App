using App.Domain.Contracts;
using App.Domain.Contracts.Adapter;
using App.Domain.Entities;
using App.Domain.Entities.Cartao;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Service
{
    public class PagamentoService : IPagamentoService
    {
        private readonly IPagamentoAdapter _pagamentoAdapter;

        public PagamentoService(IPagamentoAdapter pagamentoAdapter)
        {
            _pagamentoAdapter = pagamentoAdapter;
        }

        public int ProcessarPagamento(CartaoModel cartao, decimal valor)
        {
            if (cartao == null)
                throw new CustomException() { mensagemErro = "Cartão não informado" };

            if (string.IsNullOrEmpty(cartao.NomeCartao))
                throw new CustomException() { mensagemErro = "Nome do Cartão não informado" };

            if (string.IsNullOrEmpty(cartao.NumeroCartao))
                throw new CustomException() { mensagemErro = "Carão não informado" };

            if (Convert.ToDateTime(cartao.DataVencimentoCartao).Date < DateTime.Now.Date)
                throw new CustomException() { mensagemErro = "Cartão vencido!" };

            if (cartao.SenhaCartao.Trim().Length < 3 || Convert.ToInt32(cartao.SenhaCartao) > 999)
                throw new CustomException() { mensagemErro = "Cartão não informado" };

            return _pagamentoAdapter.EfetuarPagamento(cartao, valor);
        }
    }
}
