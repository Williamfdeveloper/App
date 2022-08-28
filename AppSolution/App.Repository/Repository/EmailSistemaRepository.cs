using App.Domain.Contracts.Repository;
using App.Domain.Entities;
using App.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Repository.Repository
{
    public class EmailSistemaRepository : IEmailSistemaRepository
    {
        DefaultContext _context;
        public EmailSistemaRepository(DefaultContext context)
        {
            _context = context;
            //var options = new MemoryCacheEntryOptions() { SlidingExpiration = TimeSpan.FromHours(2) };
            //QueryCacheManager.DefaultMemoryCacheEntryOptions = options;
        }

        public bool Atualizar(EmailSistema Email)
        {
            _context.EmailSistema.Update(Email);
            if (_context.SaveChanges() > 0)
                return true;
            else
                return false;
        }

        public EmailSistema BuscarEmail(string email)
        {
            return _context.EmailSistema.Where(c => c.UsernameEmail.Equals(email)).FirstOrDefault();
        }

        public EmailSistema BuscarEmail(Guid emailID)
        {
            return _context.EmailSistema.Where(c => c.ID.Equals(emailID)).FirstOrDefault();
        }

        public EmailSistema BuscarEmail(int tipo)
        {
            return _context.EmailSistema.Where(c => c.TipodEmail.Equals(tipo)).FirstOrDefault();
        }

        public IList<EmailSistema> BuscarEmails()
        {
            return _context.EmailSistema.ToList();
        }

        public bool Salvar(EmailSistema Email)
        {
            _context.EmailSistema.Add(Email);
            if (_context.SaveChanges() > 0)
                return true;
            else
                return false;
        }
    }
}
