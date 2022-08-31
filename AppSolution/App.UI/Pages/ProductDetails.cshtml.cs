using App.Domain.Contracts;
using App.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace App.UI.Pages
{
    public class ProductDetailsModel : PageModel
    {
        private readonly ILogger<ProductDetailsModel> _logger;
        private readonly IProdutoApiService _produtoApiService;

        public ProductDetailsModel(ILogger<ProductDetailsModel> logger, IProdutoApiService produtoApiService)
        {
            _produtoApiService = produtoApiService;
            _logger = logger;
        }

        public Produto Produto { get; set; }


        public void OnGet(int Codigo)
        {
            Produto = _produtoApiService.BuscarProduto(Codigo);
        }
    }
}
