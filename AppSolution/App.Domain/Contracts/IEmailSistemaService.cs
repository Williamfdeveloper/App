using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Contracts
{
    public interface IEmailSistemaService
    {
        bool EnviarConfirmacaoEmail(IdentityUser usuario, string callbackUrl);
        bool EnviarEmailResetSenha(IdentityUser usuario, string callbackUrl);
        public bool EnviarEmail(string EmailPara, string Assunto, string mensagem);
    }
}
