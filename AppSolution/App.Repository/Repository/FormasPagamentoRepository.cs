using App.Domain.Contracts.Repository;
using App.Domain.Entities;
using App.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Repository.Repository
{
    public class FormasPagamentoRepository : IFormasPagamentoRepository
    {
        DefaultContext _context;
        public FormasPagamentoRepository(DefaultContext context)
        {
            _context = context;
        }
        public bool Atualizar(FormaPagamento Forma)
        {
            _context.FormaPagamento.Update(Forma).Property(x => x.CodigoFormaPagamento).IsModified = false;
            if (_context.SaveChanges() > 0)
                return true;
            else
                return false;
        }

        public FormaPagamento Buscar(int FormaID)
        {
            return _context.FormaPagamento.Where(c => c.CodigoFormaPagamento.Equals(FormaID)).FirstOrDefault();
        }

        public IList<FormaPagamento> BuscarLista()
        {
            return _context.FormaPagamento.Where(c => c.Ativa.Equals(true)).ToList();
        }

        public bool Salvar(FormaPagamento Forma)
        {
            _context.FormaPagamento.Add(Forma);
            if (_context.SaveChanges() > 0)
                return true;
            else
                return false;
        }
    }
}
