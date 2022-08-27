using App.Domain.Contracts;
using App.Domain.Contracts.Repository;
using App.Domain.Entities;
using System.Collections.Generic;

namespace App.Domain.Service
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public bool AtualizarProduto(Produto produto)
        {
            return _produtoRepository.AtualizarProduto(produto);
        }

        public Produto BuscarProduto(int codigoProduto)
        {
            return _produtoRepository.BuscarProduto(codigoProduto);
        }

        public bool DeletarProduto(Produto produto)
        {
            return _produtoRepository.DeletarProduto(produto);
        }

        public IList<Produto> ListarProdutos()
        {
            return _produtoRepository.ListarProdutos();
        }

        public bool SalvarProduto(Produto produto)
        {
            return _produtoRepository.SalvarProduto(produto);
        }
    }
}
