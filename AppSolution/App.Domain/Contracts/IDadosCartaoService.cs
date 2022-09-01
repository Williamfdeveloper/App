using App.Domain.Entities;
using App.Domain.Entities.Cartao;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Contracts
{
    public interface IDadosCartaoService
    {
        bool AdicionarCartao(CartaoModel cartao, Usuario usuario);
        Task<bool> DeletarCartao(string numero);
        Task<DadosCartao>  BuscarCartao(string codigoUsuario, int codigo);
        Task<IList<DadosCartao>> BuscarListaCartoes(string codigoUsuario);

    }
}
