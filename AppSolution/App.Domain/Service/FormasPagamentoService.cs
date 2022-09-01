using App.Domain.Contracts;
using App.Domain.Contracts.Repository;
using App.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Service
{
    public class FormasPagamentoService : IFormasPagamentoService
    {
        private readonly IFormasPagamentoRepository _IFormasPagamentoRepository;
        public FormasPagamentoService(IFormasPagamentoRepository IFormasPagamentoRepository)
        {
            _IFormasPagamentoRepository = IFormasPagamentoRepository;
        }

        public bool AplicarDesconto(ref Pedido pedido)
        {
            if (pedido.FormaPagamento == null)
                return true;

            if (pedido.FormaPagamento.ValorDesconto == 0)
                return true;

            if (pedido.FormaPagamento.TipoDesconto == (int)EnumTipo.TipoDesconto.Porcentagem)
            {
                pedido.ValorTotalComDesconto = (pedido.ValorTotal * (pedido.FormaPagamento.ValorDesconto / 100));
                pedido.ValorTotal = pedido.ValorTotal - pedido.ValorTotalComDesconto;
            }
            else
            {
                pedido.ValorTotalComDesconto = (pedido.ValorTotal - pedido.FormaPagamento.ValorDesconto);
                pedido.ValorTotal = pedido.ValorTotal - pedido.ValorTotalComDesconto;
            }

            return true;

        }

        public bool Atualizar(FormaPagamento Forma)
        {
            if (Forma == null)
                throw new CustomException() { mensagemErro = $"Fomar da pagamento invalido" };

            if (Forma.IdTipoFormaPagamento == 0)
                throw new CustomException() { mensagemErro = $"ID Fomar de pagamento não informado" };

            if (string.IsNullOrEmpty(Forma.DescricaoFormaPagamento))
                throw new CustomException() { mensagemErro = $"Descrição da forma de pagamento não informado" };

            if (Forma.QuantidadeParcelas == 0)
                Forma.QuantidadeParcelas = 1;


            return _IFormasPagamentoRepository.Atualizar(Forma);
        }

        public FormaPagamento Buscar(int FormaID)
        {
            return _IFormasPagamentoRepository.Buscar(FormaID);
        }

        public IList<FormaPagamento> BuscarLista()
        {
            return _IFormasPagamentoRepository.BuscarLista();
        }

        public bool Salvar(FormaPagamento Forma)
        {
            if (Forma == null)
                throw new CustomException() { mensagemErro = $"Fomar da pagamento invalido" };

            if (Forma.IdTipoFormaPagamento == 0)
                throw new CustomException() { mensagemErro = $"ID Fomar de pagamento não informado" };

            if (string.IsNullOrEmpty(Forma.DescricaoFormaPagamento))
                throw new CustomException() { mensagemErro = $"Descrição da forma de pagamento não informado" };

            if (Forma.QuantidadeParcelas == 0)
                Forma.QuantidadeParcelas = 1;


            return _IFormasPagamentoRepository.Salvar(Forma);
        }
    }
}
