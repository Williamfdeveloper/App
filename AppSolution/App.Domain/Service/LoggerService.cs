using App.Domain.Contracts;
using App.Domain.Contracts.Repository;
using App.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Service
{
    public class LoggerService : ILoggerService
    {
        private readonly ILoggerRepository _ILoggerRepository;

        public LoggerService(ILoggerRepository ILoggerRepository)
        {
            _ILoggerRepository = ILoggerRepository;
        }

        public CustomException InsertLog(Exception ex)
        {
            if (ex == null)
                return null;

            var cex = ex.InnerException as CustomException;

            StringBuilder sbErro = new StringBuilder();
            //sbErro.Append("Código: " + code + "\r\n\r\n");
            if (cex != null && !string.IsNullOrEmpty(cex.mensagemErro))
                sbErro.Append("CustomException: " + cex.mensagemErro + "\r\n\r\n");

            sbErro.Append("Mensagem: " + ex + "\r\n\r\n");
            sbErro.Append("InnerException: " + ex.InnerException + "\r\n\r\n");
            sbErro.Append("Source: " + ex.Source + "\r\n\r\n");
            sbErro.Append("StackTrace: " + ex.StackTrace + "\r\n\r\n");

            var LogEvento = new LogEvento()
            {
                codigoErro = ex.HResult,
                createdtime = DateTime.Now,
                exception = sbErro.ToString(),
                message = ex.Message
            };

            if (_ILoggerRepository.InsertLog(LogEvento) && cex != null)
                return cex;
            else
                return null;
        }

    }
}
