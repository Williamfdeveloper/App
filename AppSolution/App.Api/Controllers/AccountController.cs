using App.Domain;
using App.Domain.Contracts;
using App.Domain.Entities;
using App.Domain.Entities.Cartao;
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
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App.Api.Controllers
{
    [Authorize(Roles = "User")]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {

        private UserManager<Usuario> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly ILoggerService _loggerService;
        private readonly ITokenService _TokenService;
        private readonly IDadosCartaoService _dadosCartaoService;
        private readonly IEmailSistemaService _emailService;
        private readonly IAccountService _accountService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public AccountController(
            ILoggerService loggerService,
            UserManager<Usuario> usrMgr,
            ILogger<AccountController> logger,
            RoleManager<IdentityRole> RoleManager,
            ITokenService ITokenService,
            IDadosCartaoService dadosCartaoService,
            IEmailSistemaService emailService,
            SignInManager<Usuario> signInManager,
            IHttpContextAccessor httpContextAccessor,
            IAccountService accountService)
        {
            _dadosCartaoService = dadosCartaoService;
            _loggerService = loggerService;
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

                var t = _accountService.AuthenticateAsync(model);

                t.Wait();

                return Ok(t.Result);
            }
            catch (AggregateException cex) when (cex.InnerException is CustomException)
            {
                _logger.LogError(cex.ToString());
                //var retorno = Content(_loggerService.InsertLog(cex).mensagemErro);
                //retorno.StatusCode = (int)HttpStatusCode.InternalServerError;

                //return retorno;
                return BadRequest(_loggerService.InsertLog(cex).mensagemErro);
            }
            catch (Exception ex)
            {
                string erro = $"Message:{ex.Message} - StackTrace: {ex.StackTrace}";
                //var MessageError = "Erro nao catalogado, por favor verifique log da aplicação.";
                _logger.LogError(erro);
                _loggerService.InsertLog(ex);

                var retorno = Content(erro);
                retorno.StatusCode = (int)HttpStatusCode.InternalServerError;

                return retorno;
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


                var t = _accountService.CadastrarAsync(model);
                t.Wait();

                return Ok(t.Result);

            }
            catch (AggregateException cex) when (cex.InnerException is CustomException)
            {
                _logger.LogError(cex.ToString());
                //var retorno = Content(_loggerService.InsertLog(cex).mensagemErro);
                //retorno.StatusCode = (int)HttpStatusCode.InternalServerError;

                //return retorno;
                return BadRequest(_loggerService.InsertLog(cex).mensagemErro);
            }
            catch (Exception ex)
            {
                string erro = $"Message:{ex.Message} - StackTrace: {ex.StackTrace}";
                //var MessageError = "Erro nao catalogado, por favor verifique log da aplicação.";
                _logger.LogError(erro);
                _loggerService.InsertLog(ex);

                var retorno = Content(erro);
                retorno.StatusCode = (int)HttpStatusCode.InternalServerError;

                return retorno;
            }

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

        [HttpPost]
        [Route("adicionarCartao")]
        [Authorize(Roles = "User")]
        public ActionResult AdicionarCartao([FromBody] CartaoModel cartao)
        {
            try
            {
                var usuario = _userManager.FindByNameAsync(User.Identity.Name).Result;
                if (usuario == null)
                    throw new CustomException() { mensagemErro = "Usuario não identificado, por gentileza, efetue o Login." };

                var t = _dadosCartaoService.AdicionarCartao(cartao, usuario);
                t.Wait();

                if (t.Result)
                    return Ok("Cartão adicionado com sucesso!");
                else
                    return BadRequest("Falha ao adicionar cartão");
            }
            catch (AggregateException cex) when (cex.InnerException is CustomException)
            {
                _logger.LogError(cex.ToString());
                //var retorno = Content(_loggerService.InsertLog(cex).mensagemErro);
                //retorno.StatusCode = (int)HttpStatusCode.InternalServerError;

                //return retorno;
                return BadRequest(_loggerService.InsertLog(cex).mensagemErro);
            }
            catch (Exception ex)
            {
                string erro = $"Message:{ex.Message} - StackTrace: {ex.StackTrace}";
                //var MessageError = "Erro nao catalogado, por favor verifique log da aplicação.";
                _logger.LogError(erro);
                _loggerService.InsertLog(ex);

                var retorno = Content(erro);
                retorno.StatusCode = (int)HttpStatusCode.InternalServerError;

                return retorno;
            }
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
