using App.Api.Model;
using App.Domain;
using App.Domain.Contracts;
using App.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace App.Api.Controllers
{
    [Authorize(Roles = "User")]
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : Controller
    {
        private UserManager<Usuario> _userManager;
        private readonly ILogger<PedidoController> _logger;
        //private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly SignInManager<Usuario> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        private readonly ILoggerService _loggerService;
        private readonly IPedidoService _pedidoService;


        public PedidoController(
            ILoggerService loggerService,
            UserManager<Usuario> usrMgr,
            ILogger<PedidoController> logger,
            //RoleManager<IdentityRole> RoleManager,
            //SignInManager<Usuario> signInManager,
            IHttpContextAccessor httpContextAccessor,
            IPedidoService pedidoService)
        {
            _loggerService = loggerService;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            //_signInManager = signInManager;
            //_roleManager = RoleManager;
            _userManager = usrMgr;
            _pedidoService = pedidoService;
        }



        [HttpPost]
        [Route("adicionaritem")]
        public ActionResult AdicionarItem()
        {
            return Ok();
        }


        [HttpPost]
        [Route("adicionaritempedido")]
        [Authorize(Roles = "User")]
        public ActionResult AdicionarItemPedido([FromBody] ItemCarrinho item)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var usuario = _userManager.FindByNameAsync(User.Identity.Name).Result;
                if (usuario == null)
                    throw new CustomException() { mensagemErro = "Voce precisa estar logado para adicionar o item ao carrinho, por gentileza, efetue o Login." };

                Pedido pedido = null;
                var ItemContext = _session.GetString($"Pedido-{usuario.Id}");
                if (ItemContext != null)
                    pedido = JsonConvert.DeserializeObject<Pedido>(ItemContext);

                if (_pedidoService.AdicionarItemCarrinho(item.codigoProduto, item.quantidade, usuario, ref pedido))
                {
                    _session.Remove($"Pedido-{usuario.Id}");
                    _session.SetString($"Pedido-{usuario.Id}", JsonConvert.SerializeObject(pedido));
                    return Ok("Item adicionado com sucesso");
                }
                else
                {
                    return BadRequest("Ero ao adicionado item no carrinho");
                }

            }
            //catch (AggregateException cex) when (cex.InnerException is CustomException)
            catch (CustomException cex)
            {
                _logger.LogError(cex.ToString());

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
    }
}
