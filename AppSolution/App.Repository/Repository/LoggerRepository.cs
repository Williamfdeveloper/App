using App.Domain.Contracts.Repository;
using App.Domain.Entities;
using App.Repository.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Repository.Repository
{
    public class LoggerRepository : ILoggerRepository
    {
        DefaultContext _context;
        public LoggerRepository(DefaultContext context)
        {
            _context = context;
        }

        public void InsertLog(LogEvento LogEvento)
        {
            _context.LogEvento.Add(LogEvento);
            _context.SaveChanges();
        }
    }
}
