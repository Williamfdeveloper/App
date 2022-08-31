using App.Adapter;
using App.Domain.Contracts.Adapter;
using App.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace App.Adapters
{
    public class ProdutoAdapter : ApiBase, IProdutoAdapter
    {

        public bool AtualizarProduto(Produto produto)
        {
            throw new NotImplementedException();
        }

        public Produto BuscarProduto(int codigoProduto)
        {
            string URL = $"https://localhost:44350/api/produto/GetProduto";

            IDictionary<string, string> _params = new Dictionary<string, string>();

            _params.Add("codigoProduto", codigoProduto.ToString());

            if (Request<object, object>(null, null, _params, URL, HttpMethod.Get, string.Empty, out object Response, out string msgErro) == HttpStatusCode.OK)
            {
                var _response = JsonConvert.DeserializeObject<Produto>(Response.ToString());

                return _response;
            }
            else
            {
                if (!string.IsNullOrEmpty(msgErro))
                {
                    throw new Exception(msgErro);
                }

                return null;
            }
        }

        public bool DeletarProduto(Produto produto)
        {
            throw new NotImplementedException();
        }

        public IList<Produto> ListarProdutos()
        {
            string URL = $"https://localhost:44350/api/produto";

            if (Request<object, object>(null, null, null, URL, HttpMethod.Get, string.Empty, out object Response, out string msgErro) == HttpStatusCode.OK)
            {
                var _response = JsonConvert.DeserializeObject<IList<Produto>>(Response.ToString());

                return _response;
            }
            else
            {
                if (!string.IsNullOrEmpty(msgErro))
                {
                    throw new Exception(msgErro);
                }

                return null;
            }
        }

        public bool SalvarProduto(Produto produto)
        {
            throw new NotImplementedException();
        }
    }
}
