using App.Domain.Contracts;
using App.Domain.Entities;
using App.Domain.Entities.TransacaoPedido;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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


        [HttpGet]
        [Route("GerarPedidoInicial")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> GerarPedidoInicialAsync()
        {
            try
            {
                var usuario = _userManager.FindByNameAsync(User.Identity.Name).Result;
                if (usuario == null)
                    throw new CustomException() { mensagemErro = "Usuario não identificado, por gentileza, efetue o Login." };

                var pedido = _pedidoService.GerarPedidoInicial(usuario);

                if (pedido != null)
                    return Ok(pedido);
                else
                    return BadRequest("Erro ao gerar pedido inicial!");

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
        [Route("AdicionarItemPedido")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> AdicionarItemPedidoAsync([FromBody] ItemCarrinho ItemCarrinho)
        {
            try
            {
                var usuario = _userManager.FindByNameAsync(User.Identity.Name).Result;
                if (usuario == null)
                    throw new CustomException() { mensagemErro = "Usuario não identificado, por gentileza, efetue o Login." };

                if (_pedidoService.AdicionarItemPedido(usuario, ref ItemCarrinho))
                    return Ok(ItemCarrinho.pedido);
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
        [Route("AdicionarFormaPagamentoPedido")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> AdicionarFormaPagamentoPedidoAsync([FromBody] FomaPagamentoModel FomaPagamentoModel)
        {
            try
            {
                var usuario = _userManager.FindByNameAsync(User.Identity.Name).Result;
                if (usuario == null)
                    throw new CustomException() { mensagemErro = "Usuario não identificado, por gentileza, efetue o Login." };

                if (_pedidoService.AdicionarFormaPagamentoPedido(usuario, ref FomaPagamentoModel))
                    return Ok(FomaPagamentoModel.pedido);
                else
                    return BadRequest("Erro ao adicionar forma de pagamento ao pedido!");

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
        [Route("AdicionarAFilaPedido")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> AdicionarAFilaPedidoAsync([FromBody] FinalizarPedidoModel FinalizarPedidoModel)
        {
            try
            {
                var usuario = _userManager.FindByNameAsync(User.Identity.Name).Result;
                if (usuario == null)
                    throw new CustomException() { mensagemErro = "Usuario não identificado, por gentileza, efetue o Login." };
                
                if (_pedidoService.AdicionaPedidoFila(usuario, FinalizarPedidoModel))
                    return Ok("Pedido adicionado a fila de processamento");
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



        [HttpGet]
        [Route("SolicitarPedidoAtualizado")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> SolicitarPedidoAtualizadoAsync([FromQuery] int codigoPedido)
        {
            try
            {
                var pedido = _pedidoService.ConsultarPedido(codigoPedido);
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
