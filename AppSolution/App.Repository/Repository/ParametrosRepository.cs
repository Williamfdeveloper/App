using App.Domain.Contracts.Repository;
using App.Domain.Entities;
using App.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Repository.Repository
{
    public class ParametrosRepository : IParametrosRepository
    {
        DefaultContext _context;
        public ParametrosRepository(DefaultContext context)
        {
            _context = context;
        }

        public bool Atualizar(ParametrosSistema ParametrosSistema)
        {
            _context.ParametrosSistema.Update(ParametrosSistema);
            if (_context.SaveChanges() > 0)
                return true;
            else
                return false;
        }

        public ParametrosSistema BuscarParametro(string nome)
        {
            return _context.ParametrosSistema.Where(c => c.nome.Equals(nome)).FirstOrDefault();
        }

        public ParametrosSistema BuscarParametro(Guid ParametroID)
        {
            return _context.ParametrosSistema.Where(c => c.parametrosid.Equals(ParametroID)).FirstOrDefault();
        }

        public IList<ParametrosSistema> BuscarParametros()
        {
            return _context.ParametrosSistema.ToList();
        }

        public bool Salvar(ParametrosSistema ParametrosSistema)
        {
            _context.ParametrosSistema.Add(ParametrosSistema);
            if (_context.SaveChanges() > 0)
                return true;
            else
                return false;
        }
    }
}
