using App.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Contracts
{
    public interface IFormasPagamentoService
    {
        bool Salvar(FormaPagamento Forma);
        bool Atualizar(FormaPagamento Forma);
        bool AplicarDesconto(ref Pedido pedido);
        FormaPagamento Buscar(int FormaID);
        IList<FormaPagamento> BuscarLista();
    }
}
