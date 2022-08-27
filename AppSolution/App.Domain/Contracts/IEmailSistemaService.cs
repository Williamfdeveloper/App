using App.Domain.Entities;

namespace App.Domain.Contracts
{
    public interface IEmailSistemaService
    {
        bool EnviarConfirmacaoEmail(Usuario usuario, string callbackUrl);
        bool EnviarEmailResetSenha(Usuario usuario, string callbackUrl);
        public bool EnviarEmail(string EmailPara, string Assunto, string mensagem);
    }
}
