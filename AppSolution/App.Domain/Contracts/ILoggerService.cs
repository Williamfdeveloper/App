using App.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Contracts
{
    public interface ILoggerService
    {
        CustomException InsertLog(Exception ex);
    }
}
