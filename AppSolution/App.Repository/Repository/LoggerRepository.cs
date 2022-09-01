using App.Domain.Contracts.Repository;
using App.Domain.Entities;
using App.Repository.Context;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Repository.Repository
{
    public class LoggerRepository : ILoggerRepository
    {
        public readonly IServiceScopeFactory _serviceScopeFactory;
        DefaultContext _context;
        public LoggerRepository(DefaultContext context, IServiceScopeFactory serviceScopeFactory)
        {
            //_context = context;
            _context = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<DefaultContext>();
        }

        public bool InsertLog(LogEvento LogEvento)
        {
            _context.LogEvento.Add(LogEvento);
            if (_context.SaveChanges() > 0)
                return true;
            else
                return false;
        }
    }
}
