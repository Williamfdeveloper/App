using App.Domain.Contracts;
using App.Domain.Contracts.Repository;
using App.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace App.Domain.Service
{
    public class EmailSistemaService : IEmailSistemaService
    {
        IParametrosRepository _IParametrosRepository;
        IEmailSistemaRepository _IEmailSistemaRepository;
        public EmailSistemaService(IParametrosRepository IParametrosRepository, IEmailSistemaRepository IEmailSistemaRepository)
        {
            _IEmailSistemaRepository = IEmailSistemaRepository;
            _IParametrosRepository = IParametrosRepository;
        }

        public bool EnviarConfirmacaoEmail(IdentityUser usuario, string callbackUrl)
        {
            if (usuario == null)
                throw new Exception("Usuario Não encontrado para confirmação do email");

            var parametro = _IParametrosRepository.BuscarParametro(Constant.ParametroEnviarConfirmacaoEmail);
            if (parametro != null && !Convert.ToBoolean(parametro.valor))
                return false;

            return EnviarEmail(usuario, callbackUrl, "Cadastro XPTO", 2);
        }

        public bool EnviarEmail(string EmailPara, string Assunto, string mensagem)
        {
            if (string.IsNullOrEmpty(EmailPara))
                throw new Exception("Email invalido.");

            if (string.IsNullOrEmpty(Assunto))
                throw new Exception("mensagem invalido, voce deve informar um assunto do email.");

            if (string.IsNullOrEmpty(mensagem))
                throw new Exception("Mensagem invalido ou em branco.");

            var email = _IEmailSistemaRepository.BuscarEmail((int)Enum.TipoEmailSistema.Sistema);

            if (email == null) return false;

            return Execute(EmailPara, Assunto, mensagem, email);

        }

        public bool EnviarEmailResetSenha(IdentityUser usuario, string callbackUrl)
        {
            if (usuario == null)
                throw new Exception("Usuario Não encontrado para redefinição da senha");

            var parametro = _IParametrosRepository.BuscarParametro(Constant.ParametroEnviarEmailResetSenha);
            if (parametro != null && !Convert.ToBoolean(parametro.valor))
                return false;

            return EnviarEmail(usuario, callbackUrl, "Redefinição de Senha", 1);

        }
        private string GerarEmailResetSenha(IdentityUser Usuario, string UrlValidacao)
        {
            var parametroHTML = _IParametrosRepository.BuscarParametro(Constant.ParametroTemplateResetSenha);
            if (parametroHTML != null)
            {
                parametroHTML.valor = Util.DecodeBase64(parametroHTML.valor);

                parametroHTML.valor = parametroHTML.valor.Replace("{{TextBody}}", $"Olá {Usuario.UserName}. <br><br>Para redefinir a senha, clique no botão abaixo para ser redirecionado para a tela atualização de senha.");
                parametroHTML.valor = parametroHTML.valor.Replace("{{ButtonLink}}", UrlValidacao);
                parametroHTML.valor = parametroHTML.valor.Replace("{{ButtonText}}", "Redefinir");
                parametroHTML.valor = parametroHTML.valor.Replace("{{TextLinkBody}}", UrlValidacao);
                parametroHTML.valor = parametroHTML.valor.Replace("{{TextFinish}}", $"Equite XPTO.<br>Email enviado para {Usuario.Email}, caso desconheça esta solicitação, por favor disconsiderar este email!");

                return parametroHTML.valor;
            }
            return string.Empty;
        }
        private string GerarEmailConfirmacao(IdentityUser Usuario, string UrlValidacao)
        {
            var parametroHTML = _IParametrosRepository.BuscarParametro(Constant.ParametroTemplateDefault);
            if (parametroHTML != null)
            {
                parametroHTML.valor = Util.DecodeBase64(parametroHTML.valor);

                parametroHTML.valor = parametroHTML.valor.Replace("{{TextBody}}", $"Olá {Usuario.UserName}.");
                parametroHTML.valor = parametroHTML.valor.Replace("{{ButtonLink}}", UrlValidacao);
                parametroHTML.valor = parametroHTML.valor.Replace("{{TextLinkBody}}", UrlValidacao);

                return parametroHTML.valor;
            }
            return string.Empty;
        }

        public bool Execute(string emails, string subject, string message, EmailSistema emailSistema)
        {
            try
            {
                SmtpClient client = new SmtpClient(emailSistema.PrimaryDomain);
                client.UseDefaultCredentials = false;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(emailSistema.UsernameEmail, emailSistema.UsernamePassword);
                client.Port = emailSistema.PrimaryPort;

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(emailSistema.UsernameEmail, emailSistema.ServerNameDisplay);
                var _emails = emails.Split(";");
                foreach (var email in _emails)
                {
                    mailMessage.To.Add(email);
                }
                mailMessage.Body = message;
                mailMessage.Subject = subject;
                mailMessage.IsBodyHtml = true;
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                // TODO: handle exception
                throw new InvalidOperationException(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="assuntoEmail"></param>
        /// <param name="tipoValidacao">1 = Reset de senha | 2 - Confirmação email</param>
        /// <returns></returns>
        private bool EnviarEmail(IdentityUser usuario, string Url, string assuntoEmail, int tipoValidacao)
        {
            string Body = string.Empty;
            switch (tipoValidacao)
            {
                case 1:
                    Body = GerarEmailResetSenha(usuario, Url);
                    break;
                case 2:
                    Body = GerarEmailConfirmacao(usuario, Url);
                    break;
                default:
                    break;
            }

            var email = _IEmailSistemaRepository.BuscarEmail((int)Enum.TipoEmailSistema.Sistema);

            if (email == null) return false;

            return Execute(usuario.Email, assuntoEmail, Body, email);
        }
    }
}
