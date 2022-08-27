using App.Domain;
using App.Domain.Contracts;
using App.Domain.Entities;
using App.Domain.Entities.Login;
using App.Domain.Entities.Register;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {

        private UserManager<Usuario> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly ITokenService _TokenService;
        private readonly IEmailSistemaService _emailService;
        private readonly IAccountService _accountService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public AccountController(UserManager<Usuario> usrMgr,
            ILogger<AccountController> logger,
            RoleManager<IdentityRole> RoleManager,
            ITokenService ITokenService,
            IEmailSistemaService emailService,
            SignInManager<Usuario> signInManager,
            IHttpContextAccessor httpContextAccessor,
            IAccountService accountService)
        {
            _accountService = accountService;
            _httpContextAccessor = httpContextAccessor;
            _signInManager = signInManager;
            _roleManager = RoleManager;
            _userManager = usrMgr;
            _emailService = emailService;
            _TokenService = ITokenService;
            _logger = logger;
        }




        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<ActionResult> AuthenticateAsync([FromBody] Login model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();

                return Ok(_accountService.AuthenticateAsync(model).Result);
            }
            catch (CustomException cex)
            {
                //var eventLog = new LogEvento()
                //{
                //    id = Guid.NewGuid(),
                //    message = cex.mensagemErro,
                //    exception = cex.ToString(),
                //    createdtime = DateTime.Now
                //};

                //_ILoggerService.InsertLog(eventLog);
                _logger.LogError(cex.mensagemErro);
                return BadRequest(cex.mensagemErro);
            }
            catch (Exception ex)
            {
                string erroMessage = $"Message: {ex.Message} - stack Trace: {ex.StackTrace}";

                //var eventLog = new LogEvento()
                //{
                //    id = Guid.NewGuid(),
                //    message = erroMessage,
                //    exception = ex.ToString(),
                //    createdtime = DateTime.Now
                //};

                //_ILoggerService.InsertLog(eventLog);
                string erros = $"Message:{ex.Message} - StackTrace: {ex.StackTrace}";
                var MessageError = "Erro nao catalogado, por favor verifique log da aplicação.";
                _logger.LogError(erros);
                return BadRequest(MessageError);
            }
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public ActionResult Register([FromBody] Register model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var error in ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList())
                    {
                        sb.Append(error);
                    }
                    throw new CustomException() { mensagemErro = sb.ToString() };
                }


                return Ok(_accountService.CadastrarAsync(model).Result);


            }
            catch (CustomException cex)
            {
                //var eventLog = new LogEvento()
                //{
                //    id = Guid.NewGuid(),
                //    message = cex.mensagemErro,
                //    exception = cex.ToString(),
                //    createdtime = DateTime.Now
                //};

                //_ILoggerService.InsertLog(eventLog);
                _logger.LogError(cex.mensagemErro);
                return BadRequest(cex.mensagemErro);
            }
            catch (Exception ex)
            {
                string erroMessage = $"Message: {ex.Message} - stack Trace: {ex.StackTrace}";

                //var eventLog = new LogEvento()
                //{
                //    id = Guid.NewGuid(),
                //    message = erroMessage,
                //    exception = ex.ToString(),
                //    createdtime = DateTime.Now
                //};

                //_ILoggerService.InsertLog(eventLog);
                string erros = $"Message:{ex.Message} - StackTrace: {ex.StackTrace}";
                var MessageError = "Erro nao catalogado, por favor verifique log da aplicação.";
                _logger.LogError(erros);
                return BadRequest(MessageError);
            }
















            //var idUsuario = Guid.NewGuid().ToString();
            //var user = new Usuario
            //{
            //    Id = idUsuario,
            //    UserName = model.Email,
            //    Email = model.Email,
            //    CPF = model.Cpf,
            //    Nome = model.Nome,
            //    DataNascimento = model.DataNascimento,
            //    Sexo = model.Sexo == 1 ? (int)EnumTipo.Sexo.Masculino : model.Sexo == 2 ? (int)EnumTipo.Sexo.Feminino : (int)EnumTipo.Sexo.Outros,
            //    Endereco = new Endereco()
            //    {
            //        Idusuario = idUsuario,
            //        CEP = model.CEP,
            //        Bairro = model.Bairro,
            //        Cidade = model.Cidade,
            //        Estado = model.Estado,
            //        Numero = model.Numero,
            //        Rua = model.Rua
            //    }
            //};
            //var result = await _userManager.CreateAsync(user, model.Password);
            //if (result.Succeeded)
            //{
            //    _logger.LogInformation("User created a new account with password.");

            //    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            //    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            //    var request = _httpContextAccessor.HttpContext.Request;

            //    var callbackUrl = $"{request.Scheme}://{request.Host}/api/account/confirmemail?userId={user.Id}&code={code}";

            //    var _usuario = await _userManager.FindByEmailAsync(model.Email);

            //    _emailService.EnviarConfirmacaoEmail(_usuario, callbackUrl);

            //    //-------------------atribuir role ao user------------------------------
            //    var applicationRole = await _roleManager.FindByNameAsync("User");
            //    if (applicationRole != null)
            //    {
            //        IdentityResult roleResult = await _userManager.AddToRoleAsync(user, applicationRole.Name);
            //    }

            //    //var applicationRoleUserPremium = await _roleManager.FindByNameAsync("User_Premium");
            //    //if (applicationRoleUserPremium != null)
            //    //{
            //    //    IdentityResult roleResult = await _userManager.AddToRoleAsync(user, applicationRoleUserPremium.Name);
            //    //}

            //    if (_userManager.Options.SignIn.RequireConfirmedAccount)
            //    {

            //        return Ok( //$"Cadastro realizado, confirme seu email - {callbackUrl}"
            //            new RegisterResponse()
            //            {
            //                user = _usuario,
            //                urlConfirmarEmail = callbackUrl
            //            });
            //    }
            //    else
            //    {
            //        await _signInManager.SignInAsync(user, isPersistent: false);
            //        return Ok(new RegisterResponse()
            //        {
            //            user = _usuario,
            //            urlConfirmarEmail = string.Empty
            //        });
            //    }
            //}
            //foreach (var error in result.Errors)
            //{
            //    ModelState.AddModelError(string.Empty, error.Description);
            //}


            // If we got this far, something failed, redisplay form
            //return BadRequest(ModelState);


        }

        [HttpGet]
        [Route("ConfirmEmail")]
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmailAsync([FromQuery] string userId, string code)
        {
            if (userId == null || code == null)
                return BadRequest("Usuario ou Codigo nao informado");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound($"Unable to load user with ID '{userId}'.");

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return Ok(result.Succeeded ? "Thank you for confirming your email." : "Error confirming your email.");
        }

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => String.Format("Autenticado - {0}", User.Identity.Name);

        [HttpGet]
        [Route("User")]
        [Authorize(Roles = "employee,manager")]
        public string Employee() => "Funcionário";

        [HttpGet]
        [Route("User_Premium")]
        [Authorize(Roles = "manager")]
        public string Manager() => "Gerente";




    }
}
