using App.Domain.Entities.Cartao;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Contracts
{
    public interface IPagamentoService
    {
        int ProcessarPagamento(CartaoModel cartao, decimal valor);
    }
}
