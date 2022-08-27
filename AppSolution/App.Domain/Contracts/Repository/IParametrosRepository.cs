using App.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Contracts.Repository
{
    public interface IParametrosRepository
    {
        ParametrosSistema BuscarParametro(string nome);
        ParametrosSistema BuscarParametro(Guid ParametroID);
        IList<ParametrosSistema> BuscarParametros();
        bool Atualizar(ParametrosSistema ParametrosSistema);
        bool Salvar(ParametrosSistema ParametrosSistema);
    }
}
