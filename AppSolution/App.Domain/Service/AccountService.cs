using App.Domain.Contracts;
using App.Domain.Entities;
using App.Domain.Entities.Login;
using App.Domain.Entities.Register;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Service
{
    public class AccountService : IAccountService
    {
        private UserManager<Usuario> _userManager;
        private readonly ILogger<AccountService> _logger;
        private readonly ITokenService _TokenService;
        private readonly IEmailSistemaService _emailService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public AccountService(UserManager<Usuario> usrMgr,
            ILogger<AccountService> logger,
            RoleManager<IdentityRole> RoleManager,
            ITokenService ITokenService,
            IEmailSistemaService emailService,
            SignInManager<Usuario> signInManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _signInManager = signInManager;
            _roleManager = RoleManager;
            _userManager = usrMgr;
            _emailService = emailService;
            _TokenService = ITokenService;
            _logger = logger;
        }


        public async Task<RegisterResponse> CadastrarAsync(Register model)
        {
            if (model.Password != model.ConfirmPassword)
                throw new CustomException() { mensagemErro = "Senha e confirmação de senha não são iguais" };

            if (!string.IsNullOrEmpty(model.Cpf) && !Util.ValidarCPF(model.Cpf))
                throw new CustomException() { mensagemErro = "Para se cadastrar você deve informar um CPF inválido." };

            if (!string.IsNullOrEmpty(model.Email) && !Util.IsValidEmail(model.Email))
                throw new CustomException() { mensagemErro = "Para se cadastrar você deve informar um email valido!" };

            //if (!string.IsNullOrEmpty(model.Nome))
            //    throw new CustomException() { mensagemErro = "Para se cadastrar voce deve informar o nome." };

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            var idUsuario = Guid.NewGuid().ToString();
            var user = new Usuario
            {
                Id = idUsuario,
                UserName = model.Email,
                Email = model.Email,
                CPF = model.Cpf,
                Nome = model.Nome,
                DataNascimento = model.DataNascimento,
                Sexo = model.Sexo == 1 ? (int)EnumTipo.Sexo.Masculino : model.Sexo == 2 ? (int)EnumTipo.Sexo.Feminino : (int)EnumTipo.Sexo.Outros,
                Endereco = model.Endereco == null ? null : new Endereco()
                {
                    Idusuario = idUsuario,
                    CEP = model.Endereco.CEP,
                    Bairro = model.Endereco.Bairro,
                    Cidade = model.Endereco.Cidade,
                    Estado = model.Endereco.Estado,
                    Numero = model.Endereco.Numero,
                    Rua = model.Endereco.Rua
                }
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    sb.Append($"{error.Description}\n");
                }

                throw new CustomException() { mensagemErro = $"Erro ao finalizar cadastro, motivo: {sb.ToString()}" };
            }

            _logger.LogInformation("User created a new account with password.");

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var request = _httpContextAccessor.HttpContext.Request;

            var callbackUrl = $"{request.Scheme}://{request.Host}/api/account/confirmemail?userId={user.Id}&code={code}";

            var _usuario = await _userManager.FindByEmailAsync(model.Email);

            _emailService.EnviarConfirmacaoEmail(_usuario, callbackUrl);

            //-------------------atribuir role ao user------------------------------
            var applicationRole = await _roleManager.FindByNameAsync("User");
            if (applicationRole != null)
            {
                IdentityResult roleResult = await _userManager.AddToRoleAsync(user, applicationRole.Name);
            }

            //var applicationRoleUserPremium = await _roleManager.FindByNameAsync("User_Premium");
            //if (applicationRoleUserPremium != null)
            //{
            //    IdentityResult roleResult = await _userManager.AddToRoleAsync(user, applicationRoleUserPremium.Name);
            //}

            if (_userManager.Options.SignIn.RequireConfirmedEmail)
            {

                return new RegisterResponse()
                {
                    user = _usuario,
                    urlConfirmarEmail = callbackUrl
                };
            }
            else
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return new RegisterResponse()
                {
                    user = _usuario,
                    urlConfirmarEmail = string.Empty
                };
            }

        }

        public async Task<LoginResponse> AuthenticateAsync(Login model)
        {
            //if (!ModelState.IsValid)
            //    return View();

            if (string.IsNullOrEmpty(model.Email))
                throw new CustomException() { mensagemErro = $"Email nao informado" };

            if (string.IsNullOrEmpty(model.Password))
                throw new CustomException() { mensagemErro = $"Email nao informado" };


            //var psw = Util.Decrypt(model.Password);

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, lockoutOnFailure: false);
            if (!result.Succeeded)
                throw new CustomException() { mensagemErro = $"Usuario não encontrado." };

            _logger.LogInformation("User logged in.");

            // Recupera o usuário
            var user = await _userManager.FindByEmailAsync(model.Email);

            // Verifica se o usuário existe
            if (user == null)
                throw new CustomException() { mensagemErro = "Usuário não encontrado" };

            var claims = await GetRolesAsync(user);

            // Gera o Token
            var token = _TokenService.GenerateToken(user, claims);

            // Oculta a senha
            user.PasswordHash = "";

            return new LoginResponse()
            {
                user = user,
                token = token,
            };
        }

        public async Task<IEnumerable<Claim>> GetRolesAsync(Usuario user)
        {
            IList<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.UserName.ToString()));
            var roles = _userManager.GetRolesAsync(user).Result.ToArray();
            
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }
    }
}
