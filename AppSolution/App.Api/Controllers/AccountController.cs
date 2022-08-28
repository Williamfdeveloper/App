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
        private readonly ILoggerService _loggerService;
        private readonly ITokenService _TokenService;
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
            IEmailSistemaService emailService,
            SignInManager<Usuario> signInManager,
            IHttpContextAccessor httpContextAccessor,
            IAccountService accountService)
        {
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
                _loggerService.InsertLog(cex);
                return BadRequest(cex);
            }
            catch (Exception ex)
            {
                string erros = $"Message:{ex.Message} - StackTrace: {ex.StackTrace}";
                var MessageError = "Erro nao catalogado, por favor verifique log da aplicação.";
                _logger.LogError(erros);
                _loggerService.InsertLog(ex);
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


                var t = _accountService.CadastrarAsync(model);
                t.Wait();

                return Ok(t.Result);

            }
            catch (AggregateException cex) when (cex.InnerException is CustomException)
            {
                _logger.LogError(cex.ToString());
                _loggerService.InsertLog(cex);
                return BadRequest(cex);
            }
            catch (Exception ex)
            {
                string erros = $"Message:{ex.Message} - StackTrace: {ex.StackTrace}";
                var MessageError = "Erro nao catalogado, por favor verifique log da aplicação.";
                _logger.LogError(erros);
                _loggerService.InsertLog(ex);
                return BadRequest(MessageError);
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
