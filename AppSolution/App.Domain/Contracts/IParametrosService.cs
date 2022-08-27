using App.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Contracts
{
    public interface IParametrosService
    {
        ParametrosSistema BuscarParametro(string nome);
        ParametrosSistema BuscarParametro(Guid ParametroID);
        IList<ParametrosSistema> BuscarParametros();
        bool Atualizar(Guid id, string valor, string descricao);
        bool Salvar(string nome, string valor, string descricao, int tipo);
    }
}
