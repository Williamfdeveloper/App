using App.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Contracts.Repository
{
    public interface IFormasPagamentoRepository
    {
        bool Salvar(FormaPagamento Forma);
        bool Atualizar(FormaPagamento Forma);
        FormaPagamento Buscar(int FormaID);
        IList<FormaPagamento> BuscarLista();

    }
}
