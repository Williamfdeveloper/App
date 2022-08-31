using App.Domain.Contracts;
using App.Domain.Contracts.Adapter;
using App.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Service
{
    public class ProdutoApiService : IProdutoApiService
    {
        public readonly IProdutoAdapter _produtoAdapter;

        public ProdutoApiService(IProdutoAdapter produtoAdapter)
        {
            _produtoAdapter = produtoAdapter;
        }

        public bool AtualizarProduto(Produto produto)
        {
            throw new NotImplementedException();
        }

        public Produto BuscarProduto(int codigoProduto)
        {
            return _produtoAdapter.BuscarProduto(codigoProduto);
        }

        public bool DeletarProduto(Produto produto)
        {
            throw new NotImplementedException();
        }

        public IList<Produto> ListarProdutos()
        {
            return _produtoAdapter.ListarProdutos();
        }

        public bool SalvarProduto(Produto produto)
        {
            throw new NotImplementedException();
        }
    }
}
