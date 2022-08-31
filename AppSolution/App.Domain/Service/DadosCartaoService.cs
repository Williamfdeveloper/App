using App.Domain.Contracts;
using App.Domain.Contracts.Repository;
using App.Domain.Entities;
using App.Domain.Entities.Cartao;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Service
{
    public class DadosCartaoService : IDadosCartaoService
    {
        private readonly IDadosCartaoRepository _cartaoRepository;

        public DadosCartaoService(IDadosCartaoRepository cartaoRepository)
        {
            _cartaoRepository = cartaoRepository;

        }

        public async Task<bool> AdicionarCartao(CartaoModel cartao, Usuario usuario)
        {
            if (cartao == null)
                throw new CustomException() { mensagemErro = "Cartão não informado" };

            if (string.IsNullOrEmpty(cartao.NumeroCartao) || Util.checkLuhn(cartao.NumeroCartao))
                throw new CustomException() { mensagemErro = "Numero do cartão inválido" };

            if (Convert.ToDateTime(cartao.DataVencimentoCartao) <= DateTime.Now.Date)
                throw new CustomException() { mensagemErro = "Vencimento do cartão esta ultrapassado."};

            if (cartao.SenhaCartao.Length < 3 || Convert.ToInt32(cartao.SenhaCartao) > 999)
                throw new CustomException() { mensagemErro = "Senha do cartão inválida" };

            if (string.IsNullOrEmpty(cartao.NomeCartao))
                throw new CustomException() { mensagemErro = "Nome impresso no cartão deve ser informado" };

            var dadosCartao = new DadosCartao()
            {
                CodigoUsuario = usuario.Id,
                NumeroCartao = Util.ConverterNumeroCartao(cartao.NumeroCartao),
                NomeCartao = cartao.NomeCartao,
                DataVencimentoCartao = cartao.DataVencimentoCartao,
            };

            //hash do cartão é retornado pela operadora, onde sera salvo e utilizado em transações futuras.
            dadosCartao.HashCartao = Util.Encrypt(JsonConvert.SerializeObject(dadosCartao));

            return _cartaoRepository.Salvar(dadosCartao);
        }

        public async Task<DadosCartao> BuscarCartao(string codigoUsuario, int codigo)
        {
            return _cartaoRepository.Buscar(codigoUsuario, codigo);
        }

        public async Task<IList<DadosCartao>> BuscarListaCartoes(string codigoUsuario)
        {
            if (string.IsNullOrEmpty(codigoUsuario))
                throw new CustomException() { mensagemErro = "Codigo do usuario inválido" };


            return _cartaoRepository.BuscarLista(codigoUsuario);
        }

        public async Task<bool> DeletarCartao(string numero)
        {
            throw new NotImplementedException();
        }
    }
}
