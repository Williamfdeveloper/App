using App.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Contracts.Repository
{
    public interface IDadosCartaoRepository
    {
        bool Salvar(DadosCartao dadosCartao);
        bool Deletar(DadosCartao dadosCartao);
        List<DadosCartao> BuscarLista(string codigoUsuario);
        DadosCartao Buscar(string codigoUsuario, int codigo);

    }
}
