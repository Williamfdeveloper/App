using App.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Contracts.Repository
{
    public interface IEmailSistemaRepository
    {
        EmailSistema BuscarEmail(string email);
        EmailSistema BuscarEmail(Guid emailID);
        EmailSistema BuscarEmail(int tipo);
        IList<EmailSistema> BuscarEmails();
        bool Atualizar(EmailSistema ParametrosSistema);
        bool Salvar(EmailSistema ParametrosSistema);
    }
}
