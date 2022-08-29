using App.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Contracts.Repository
{
    public interface ILoggerRepository
    {
        bool InsertLog(LogEvento LogEvento);
    }
}
