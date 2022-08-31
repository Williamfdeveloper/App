using App.Domain.Contracts;
using App.Domain.Contracts.Adapter;
using App.Domain.Entities;
using App.Domain.Entities.Login;
using App.Domain.Entities.Register;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Service
{
    public class AccountApiService : IAccountApiService
    {
        //private readonly ILogger<AccountService> _logger;
        //private readonly ITokenService _TokenService;
        //private readonly IEmailSistemaService _emailService;
        private readonly IAccountAdapter _accountAdapter;
        //private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountApiService(
            //ILogger<AccountService> logger,
            //ITokenService ITokenService,
            //IEmailSistemaService emailService,
            //IHttpContextAccessor httpContextAccessor,
            IAccountAdapter accountAdapter)
        {
            _accountAdapter = accountAdapter;
            //_httpContextAccessor = httpContextAccessor;
            //_emailService = emailService;
            //_TokenService = ITokenService;
            //_logger = logger;
        }

        public LoginResponse AuthenticateApi(Login model)
        {
            if (model == null)
                throw new CustomException() { mensagemErro = "Login nao informado" };

            if (string.IsNullOrEmpty(model.Password))
                throw new CustomException() { mensagemErro = "Senha nao informado" };

            if (string.IsNullOrEmpty(model.Email))
                throw new CustomException() { mensagemErro = "Email nao informado" };

            return _accountAdapter.AuthenticateApi(model);

        }

        public RegisterResponse CadastrarApi(Register model)
        {
            if (model == null)
                throw new CustomException() { mensagemErro = "Login nao informado" };

            if (string.IsNullOrEmpty(model.Password))
                throw new CustomException() { mensagemErro = "Senha nao informado" };

            if (string.IsNullOrEmpty(model.ConfirmPassword))
                throw new CustomException() { mensagemErro = "Senha nao informado" };

            if (!string.IsNullOrEmpty(model.Email) && !Util.IsValidEmail(model.Email))
                throw new CustomException() { mensagemErro = "Email nao informado" };

            if (model.Password != model.ConfirmPassword)
                throw new CustomException() { mensagemErro = "Senha e confirmação de senha não são iguais" };

            if (!string.IsNullOrEmpty(model.Cpf) && !Util.ValidarCPF(model.Cpf))
                throw new CustomException() { mensagemErro = "Para se cadastrar você deve informar um CPF inválido." };

            return _accountAdapter.CadastrarApi(model);
        }
    }
}
