using App.Domain.Contracts;
using App.Domain.Entities;
using App.Domain.Entities.TransacaoPedido;
using App.Domain.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;

namespace App.UI.Pages
{
    public class ProductDetailsModel : PageModel
    {
        private readonly ILogger<ProductDetailsModel> _logger;
        private readonly IProdutoApiService _produtoApiService;
        private readonly IPedidoApiService _pedidoApiService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;

        public ProductDetailsModel(ILogger<ProductDetailsModel> logger, IProdutoApiService produtoApiService, IHttpContextAccessor httpContextAccessor,
            UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, PedidoApiService pedidoApiService)
        {
            _produtoApiService = produtoApiService;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _signInManager = signInManager;
            _userManager = userManager;
            _pedidoApiService = pedidoApiService;
        }

        public Produto Produto { get; set; }


        public void OnGet(int Codigo)
        {
            Produto = _produtoApiService.BuscarProduto(Codigo);
        }

        public void OnPost(int CodigoProduto, int quantidade)
        {
            try
            {
                if (CodigoProduto <= 0)
                    throw new CustomException() { mensagemErro = "Codigo item invalido" };

                var usuario = _userManager.FindByNameAsync(User.Identity.Name).Result;
                if (usuario == null)
                    throw new CustomException() { mensagemErro = "Voce precisa estar logado para adicionar o item ao carrinho, por gentileza, efetue o Login." };

                var token = _session.GetString($"bearerTokem-{usuario.Id}");
                if (string.IsNullOrEmpty(token))
                    throw new CustomException() { mensagemErro = "Tokem Invalido - Por gentileza, efetue o Login." };

                var item = _produtoApiService.BuscarProduto(CodigoProduto);
                if (item == null)
                    throw new CustomException() { mensagemErro = "Item não encontrado" };

                ItemCarrinho itemCarrinho = new ItemCarrinho();
                var ItemContext = _session.GetString($"Pedido-{usuario.Id}");
                if (ItemContext == null)
                {
                    itemCarrinho.pedido = _pedidoApiService.GerarPedidoInicial(token);
                    itemCarrinho.codigoProduto = CodigoProduto;
                    itemCarrinho.quantidade = quantidade;

                    itemCarrinho.pedido = _pedidoApiService.AdicionarItemPedido(token, itemCarrinho);
                    _session.SetString($"Pedido-{usuario.Id}", JsonConvert.SerializeObject(ItemContext));
                }
                else
                {
                    itemCarrinho = JsonConvert.DeserializeObject<ItemCarrinho>(ItemContext);
                    itemCarrinho.pedido = _pedidoApiService.AdicionarItemPedido(token, itemCarrinho);

                    _session.Remove($"Pedido-{usuario.Id}");
                    _session.SetString($"Pedido-{usuario.Id}", JsonConvert.SerializeObject(ItemContext));
                }
            }
            catch (CustomException cex)
            {
                _logger.LogError(cex.ToString());
                string erro = $"Code: {cex.httpStatusCode}- Message:{cex.mensagemErro} - StackTrace: {cex.StackTrace}";
            }
            catch (Exception ex)
            {
                string erro = $"Message:{ex.Message} - StackTrace: {ex.StackTrace}";
                _logger.LogError(erro);
            }
        }
    }
}
