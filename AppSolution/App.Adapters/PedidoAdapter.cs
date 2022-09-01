using App.Adapter;
using App.Domain.Contracts.Adapter;
using App.Domain.Entities;
using App.Domain.Entities.TransacaoPedido;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace App.Adapters
{
    public class PedidoAdapter : ApiBase,IPedidoAdapter
    {

        public Pedido AdicionarAFilaPedido(string token, FinalizarPedidoModel FinalizarPedidoModel)
        {
            string URL = $"https://localhost:44350/api/produto/GetProduto";

            var httpStatusCode = Request<FinalizarPedidoModel, object>(FinalizarPedidoModel, null, null, URL, HttpMethod.Get, token, out object Response, out string msgErro);
            if (httpStatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<Pedido>(Response.ToString());
            }
            else
            {
                if (!string.IsNullOrEmpty(msgErro))
                {
                    throw new CustomException() { mensagemErro = msgErro, httpStatusCode = httpStatusCode };
                }

                return null;
            }
        }

        public Pedido AdicionarFormaPagamentoPedido(string token, FomaPagamentoModel FomaPagamentoModel)
        {
            string URL = $"https://localhost:44350/api/produto/GetProduto";
            var httpStatusCode = Request<FomaPagamentoModel, object>(FomaPagamentoModel, null, null, URL, HttpMethod.Get, token, out object Response, out string msgErro);
            if (httpStatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<Pedido>(Response.ToString());
            }
            else
            {
                if (!string.IsNullOrEmpty(msgErro))
                {
                    throw new CustomException() { mensagemErro = msgErro, httpStatusCode = httpStatusCode };
                }

                return null;
            }
        }

        public Pedido AdicionarItemPedido(string token, ItemCarrinho ItemCarrinho)
        {
            string URL = $"https://localhost:44350/api/produto/GetProduto";

            var httpStatusCode = Request<ItemCarrinho, object>(ItemCarrinho, null, null, URL, HttpMethod.Get, token, out object Response, out string msgErro);
            if (httpStatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<Pedido>(Response.ToString());
            }
            else
            {
                if (!string.IsNullOrEmpty(msgErro))
                {
                    throw new CustomException() { mensagemErro = msgErro, httpStatusCode = httpStatusCode };
                }

                return null;
            }
        }

        public Pedido GerarPedidoInicial(string token)
        {
            string URL = $"https://localhost:44350/api/pedido/GerarPedidoInicial";

            var httpStatusCode = Request<object, object>(null, null, null, URL, HttpMethod.Get, token, out object Response, out string msgErro);
            if (httpStatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<Pedido>(Response.ToString());
            }
            else
            {
                if (!string.IsNullOrEmpty(msgErro))
                {
                    throw new CustomException() { mensagemErro = msgErro, httpStatusCode = httpStatusCode };
                }

                return null;
            }
        }

        public Pedido SolicitarPedidoAtualizado(string token, int codigoPedido)
        {
            string URL = $"https://localhost:44350/api/produto/GetProduto";

            var httpStatusCode = Request<object, object>(null, null, null, URL, HttpMethod.Get, token, out object Response, out string msgErro);
            if (httpStatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<Pedido>(Response.ToString());
            }
            else
            {
                if (!string.IsNullOrEmpty(msgErro))
                {
                    throw new CustomException() { mensagemErro = msgErro, httpStatusCode = httpStatusCode };
                }

                return null;
            }
        }
    }
}
