using App.Domain.Contracts.Repository;
using App.Domain.Entities;
using App.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Repository.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        DefaultContext _context;
        public ProdutoRepository(DefaultContext context)
        {
            _context = context;
        }

        public bool AtualizarProduto(Produto produto)
        {
            _context.Produto.Update(produto);
            if (_context.SaveChanges() > 0)
                return true;
            else
                return false;
        }

        public Produto BuscarProduto(int codigoProduto)
        {
            return _context.Produto.Where(c => c.CodigoProduto.Equals(codigoProduto)).FirstOrDefault();
        }

        public bool DeletarProduto(Produto produto)
        {
            throw new NotImplementedException();
        }

        public IList<Produto> ListarProdutos()
        {
            return _context.Produto.ToList();
        }

        public bool SalvarProduto(Produto produto)
        {
            _context.Produto.Add(produto);
            if (_context.SaveChanges() > 0)
                return true;
            else
                return false;
        }
    }
}
