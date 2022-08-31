using App.Domain.Entities.Cartao;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Contracts.Adapter
{
    public interface IPagamentoAdapter
    {
        int EfetuarPagamento(CartaoModel cartao, decimal valor);
    }
}
