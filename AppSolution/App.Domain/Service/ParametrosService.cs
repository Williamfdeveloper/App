using App.Domain.Contracts;
using App.Domain.Contracts.Repository;
using App.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Service
{
    public class ParametrosService : IParametrosService
    {
        private readonly IParametrosRepository _IParametrosRepository;
        public ParametrosService(IParametrosRepository IParametrosRepository)
        {
            _IParametrosRepository = IParametrosRepository;
        }

        public bool Atualizar(Guid id, string valor, string descricao)
        {
            ParametrosSistema parametros = null;

            if (id == Guid.Empty)
                throw new CustomException() { mensagemErro = "Codigo Invalido" };

            if (string.IsNullOrEmpty(valor))
                throw new CustomException() { mensagemErro = "Valor não Informado" };

            if (string.IsNullOrEmpty(descricao))
                throw new CustomException() { mensagemErro = "Descricao não Informado" };

            parametros = BuscarParametro(id);
            switch (parametros.tipocampo)
            {
                case 1: //string

                    if (Util.IsBase64String(parametros.valor))
                        parametros.valor = Util.EncodeBase64(parametros.valor);
                    else
                        parametros.valor = valor;

                    break;
                case 2: //inteiro
                    if (!Int32.TryParse(valor, out int _int))
                        throw new CustomException() { mensagemErro = "Tipo do Valor Incorreto, Aceito somente numeros para este parametro" };

                    parametros.valor = _int.ToString();
                    break;
                case 3: //booleano
                    if (!bool.TryParse(valor, out bool _bool))
                        throw new CustomException() { mensagemErro = "Tipo do Valor Incorreto, Aceito somente True/False ou 0/1 para este parametro" };

                    parametros.valor = _bool.ToString();
                    break;
                default:
                    parametros.valor = valor;
                    break;
            }

            parametros.descricao = descricao;


            return _IParametrosRepository.Atualizar(parametros);
        }

        public ParametrosSistema BuscarParametro(string nome)
        {
            return _IParametrosRepository.BuscarParametro(nome);
        }

        public ParametrosSistema BuscarParametro(Guid ParametroID)
        {
            return _IParametrosRepository.BuscarParametro(ParametroID);
        }

        public IList<ParametrosSistema> BuscarParametros()
        {
            return _IParametrosRepository.BuscarParametros();
        }

        public bool Salvar(string nome, string valor, string descricao, int tipo)
        {
            if (string.IsNullOrEmpty(nome))
                throw new CustomException() { mensagemErro = "Nome não Informado" };
            if (string.IsNullOrEmpty(valor))
                throw new CustomException() { mensagemErro = "Valor não Informado" };

            if (string.IsNullOrEmpty(descricao))
                throw new CustomException() { mensagemErro = "Descricao não Informado" };

            if (tipo == 0)
                throw new CustomException() { mensagemErro = "Tipo não Informado" };

            ParametrosSistema parametros = new ParametrosSistema();

            parametros.parametrosid = Guid.NewGuid();
            parametros.nome = nome;
            parametros.valor = valor;
            parametros.descricao = descricao;
            parametros.tipocampo = tipo;

            return _IParametrosRepository.Salvar(parametros);
        }
    }
}
