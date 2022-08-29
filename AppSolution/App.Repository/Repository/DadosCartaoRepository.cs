using App.Domain.Contracts.Repository;
using App.Domain.Entities;
using App.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Repository.Repository
{
    public class DadosCartaoRepository : IDadosCartaoRepository
    {
        DefaultContext _context;
        public DadosCartaoRepository(DefaultContext context)
        {
            _context = context;
        }

        public DadosCartao Buscar(string codigoUsuario, int codigo)
        {
            return _context.DadosCartao.Where(c => c.CodigoUsuario.Equals(codigoUsuario) && c.CodigoDadosCartao.Equals(codigo)).FirstOrDefault();
        }

        public List<DadosCartao> BuscarLista(string codigoUsuario)
        {
            return _context.DadosCartao.Where(c => c.CodigoUsuario.Equals(codigoUsuario)).ToList();
        }

        public bool Deletar(DadosCartao dadosCartao)
        {
            throw new NotImplementedException();
        }

        public bool Salvar(DadosCartao dadosCartao)
        {
            _context.DadosCartao.Add(dadosCartao);
            if (_context.SaveChanges() > 0)
                return true;
            else
                return false;
        }
    }
}
