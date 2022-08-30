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
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : Controller
    {
        private UserManager<Usuario> _userManager;
        private readonly ILogger<PedidoController> _logger;
        private readonly ILoggerService _loggerService;
        private readonly IPedidoService _pedidoService;


        public PedidoController(
            ILoggerService loggerService,
            UserManager<Usuario> usrMgr,
            ILogger<PedidoController> logger,
            IPedidoService pedidoService)
        {
            _loggerService = loggerService;
            _logger = logger;
            _userManager = usrMgr;
            _pedidoService = pedidoService;
        }


        [HttpPost]
        [Route("CaptarPedido")]
        [Authorize(Roles = "User")]
        public ActionResult CaptarPedido([FromBody] Pedido pedido)
        {
            try
            {
                var t = _pedidoService.CaptarPedido(ref pedido);
                t.Wait();

                if (t.Result)
                    return Ok(pedido);
                else
                    return BadRequest("Erro ao finalizar pedido!");

            }
            catch (AggregateException cex) when (cex.InnerException is CustomException)
            {
                _logger.LogError(cex.ToString());

                return BadRequest(_loggerService.InsertLog(cex).mensagemErro);
            }
            catch (Exception ex)
            {
                string erro = $"Message:{ex.Message} - StackTrace: {ex.StackTrace}";
                _logger.LogError(erro);
                _loggerService.InsertLog(ex);

                var retorno = Content(erro);
                retorno.StatusCode = (int)HttpStatusCode.InternalServerError;

                return retorno;
            }
        }

        [HttpPost]
        [Route("VerificarSituacaoPedido")]
        [Authorize(Roles = "User")]
        public ActionResult VerificarSituacaoPedido([FromBody] int codigoPedido)
        {
            try
            {
                var t = _pedidoService.ConsultarPedido(codigoPedido);
                t.Wait();

                var pedido = t.Result;

                if (pedido != null)
                    return Ok(pedido);
                else
                    return BadRequest("Erro ao finalizar pedido!");
            }
            catch (AggregateException cex) when (cex.InnerException is CustomException)
            {
                _logger.LogError(cex.ToString());

                return BadRequest(_loggerService.InsertLog(cex).mensagemErro);
            }
            catch (Exception ex)
            {
                string erro = $"Message:{ex.Message} - StackTrace: {ex.StackTrace}";
                _logger.LogError(erro);
                _loggerService.InsertLog(ex);

                var retorno = Content(erro);
                retorno.StatusCode = (int)HttpStatusCode.InternalServerError;

                return retorno;
            }
        }
    }
}
